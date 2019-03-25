package org.modelio.microservicesnetcore.psm.generator.orchestrator;

import java.util.ArrayList;
import java.util.List;

import org.modelio.api.modelio.model.IModelingSession;
import org.modelio.api.modelio.model.ITransaction;
import org.modelio.api.module.IModule;
import org.modelio.metamodel.uml.infrastructure.ModelElement;
import org.modelio.vcore.smkernel.mapi.MObject;

import com.smag.applicativearchitecture.audit.AuditRule;

import org.modelio.microservicesnetcore.api.ModuleConstants;
import org.modelio.microservicesnetcore.api.ModuleTagType;
import org.modelio.microservicesnetcore.code.generator.GenerateIRepoProjectCodeHandler;
import org.modelio.microservicesnetcore.code.generator.GenerateRepoProjectCodeHandler;
import org.modelio.microservicesnetcore.helper.ModuleHelper;
import org.modelio.microservicesnetcore.psm.generator.handler.GeneratePsmModelDetailHandler;
import org.modelio.microservicesnetcore.psm.generator.handler.GeneratePsmModelHandler;
import org.modelio.modeliotools.treevisitor.OwnedVisitor;
import org.modelio.microservicesnetcore.helper.PimPsmMapper;
import org.modelio.microservicesnetcore.helper.PsmBuilder;
import org.modelio.metamodel.uml.statik.Package;

public class GenerateRepoProjectCodeOrchestrator {

	private ModelElement _umlPsmPackage = null;
	private IModule _module;
	private IModelingSession _session;
	
	public GenerateRepoProjectCodeOrchestrator(IModule module)
	{
		this._module = module;
		this._session  =module.getModuleContext().getModelingSession();
		
	}
	
	public void Execute(MObject selectedModelPsm) 
	{
			
		Package domain = (Package)((Package)selectedModelPsm).getOwner();
		Package application = (Package)domain.getOwner();
		String path = domain.getTagValue(ModuleConstants.MODULE_NAME, ModuleTagType.TAG_GENERATEDIRECTORY);
		String applicationName=application.getTagValue(ModuleConstants.MODULE_NAME, ModuleTagType.TAG_NAME);
		List<String> iRepositories = new ArrayList<String>();
		
		// create IRepo Code
		GenerateIRepoProjectCodeHandler handler =new GenerateIRepoProjectCodeHandler(applicationName,domain,path,iRepositories);
		OwnedVisitor visitor = new OwnedVisitor(handler);
		visitor.process((Package)selectedModelPsm);
		
		// create Repo Code
		GenerateRepoProjectCodeHandler handler =new GenerateRepoProjectCodeHandler(applicationName,domain,path);
		visitor = new OwnedVisitor(handler);
		visitor.process((Package)selectedModelPsm);
		
		// Creation IUoW
		String iuowPath=path+"/irepository";
				
		
		
		// Creation UoW
		String uowPath=path+"/repository";	
	}
}
