package org.modelio.microservicesnetcore.psm.generator.orchestrator;

import java.util.ArrayList;
import java.util.List;

import org.modelio.api.modelio.model.IModelingSession;
import org.modelio.api.module.IModule;
import org.modelio.metamodel.uml.infrastructure.ModelElement;
import org.modelio.vcore.smkernel.mapi.MObject;


import org.modelio.microservicesnetcore.api.ModuleConstants;
import org.modelio.microservicesnetcore.api.ModuleTagType;
import org.modelio.microservicesnetcore.code.generator.GenerateIServiceProjectCodeHandler;
import org.modelio.microservicesnetcore.code.generator.GenerateServiceProjectCodeHandler;
import org.modelio.microservicesnetcore.helper.ModuleHelper;
import org.modelio.microservicesnetcore.psm.generator.handler.GeneratePsmModelDetailHandler;
import org.modelio.microservicesnetcore.psm.generator.handler.GeneratePsmModelHandler;
import org.modelio.modeliotools.treevisitor.OwnedVisitor;
import org.modelio.microservicesnetcore.helper.PimPsmMapper;
import org.modelio.microservicesnetcore.helper.PsmBuilder;
import org.modelio.metamodel.uml.statik.Package;

public class GenerateServiceProjectCodeOrchestrator {

	private ModelElement _umlPsmPackage = null;
	private IModule _module;
	private IModelingSession _session;
	
	public GenerateServiceProjectCodeOrchestrator(IModule module)
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
		
		// create IRepo Code
		GenerateIServiceProjectCodeHandler iservhandler =new GenerateIServiceProjectCodeHandler(applicationName,domain,path);
		OwnedVisitor visitor = new OwnedVisitor(iservhandler);
		visitor.process((Package)selectedModelPsm);
		
		// create Repo Code
		GenerateServiceProjectCodeHandler servhandler =new GenerateServiceProjectCodeHandler(applicationName,domain,path);
		visitor = new OwnedVisitor(servhandler);
		visitor.process((Package)selectedModelPsm);
		
	}
}
