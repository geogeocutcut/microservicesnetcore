package org.modelio.microservicesnetcore.psm.generator.orchestrator;

import java.io.File;
import java.io.FileWriter;
import java.util.ArrayList;
import java.util.List;
import java.util.Optional;

import org.modelio.api.modelio.model.IModelingSession;
import org.modelio.api.module.IModule;
import org.modelio.metamodel.uml.infrastructure.ModelElement;
import org.modelio.metamodel.uml.infrastructure.ModelTree;
import org.modelio.vcore.smkernel.mapi.MObject;


import org.modelio.microservicesnetcore.api.ModuleConstants;
import org.modelio.microservicesnetcore.api.ModuleTagType;
import org.modelio.microservicesnetcore.code.generator.GenerateIRepoProjectCodeHandler;
import org.modelio.microservicesnetcore.code.generator.GenerateIServiceProjectCodeHandler;
import org.modelio.microservicesnetcore.code.generator.GenerateModelProjectCodeHandler;
import org.modelio.microservicesnetcore.code.generator.GenerateRepoProjectCodeHandler;
import org.modelio.microservicesnetcore.code.generator.GenerateServiceProjectCodeHandler;
import org.modelio.microservicesnetcore.code.generator.GenerateWebapiProjectCodeHandler;
import org.modelio.microservicesnetcore.code.generator.IRepositoryProjectTemplate;
import org.modelio.microservicesnetcore.code.generator.RepositoryProjectTemplate;
import org.modelio.microservicesnetcore.helper.ModuleHelper;
import org.modelio.microservicesnetcore.psm.generator.handler.GeneratePsmModelDetailHandler;
import org.modelio.microservicesnetcore.psm.generator.handler.GeneratePsmModelHandler;
import org.modelio.modeliotools.treevisitor.OwnedVisitor;
import org.modelio.microservicesnetcore.helper.PimPsmMapper;
import org.modelio.microservicesnetcore.helper.PsmBuilder;
import org.modelio.microservicesnetcore.helper.PsmStereotypeValidator;
import org.modelio.metamodel.uml.statik.Package;

public class GenerateProjectsCodeOrchestrator {

	private ModelElement _umlPsmPackage = null;
	private IModule _module;
	private IModelingSession _session;
	
	public GenerateProjectsCodeOrchestrator(IModule module)
	{
		this._module = module;
		this._session  =module.getModuleContext().getModelingSession();
		
	}
	
	public void Execute(MObject selectedModelPsm) 
	{
		boolean next = true;
		List<String> repositories=new ArrayList<String>();
		List<String> services=new ArrayList<String>();
		Package domain = ((Package)selectedModelPsm);
		Package application = (Package)domain.getOwner();
		String path = domain.getTagValue(ModuleConstants.MODULE_NAME, ModuleTagType.TAG_GENERATEDIRECTORY);
		String applicationName=application.getTagValue(ModuleConstants.MODULE_NAME, ModuleTagType.TAG_NAME);
	
		ModelTree model = ((Package)selectedModelPsm).getOwnedElement().stream().filter(e -> PsmStereotypeValidator.IsPsmModelPackage(e)).findFirst().orElse(null);
		if(model!=null && model instanceof Package)
		{
			GenerateModelProjectCodeHandler handler =new GenerateModelProjectCodeHandler(applicationName,domain,path);
			OwnedVisitor visitor = new OwnedVisitor(handler);
			visitor.process((Package)model);
		}
		else
		{
			next = false;
		}
		
		ModelTree repo = ((Package)selectedModelPsm).getOwnedElement().stream().filter(e -> PsmStereotypeValidator.IsPsmRepositoryPackage(e)).findFirst().orElse(null);
		if(next && repo!=null && repo instanceof Package)
		{
			// create IRepo Code
			GenerateIRepoProjectCodeHandler irepohandler =new GenerateIRepoProjectCodeHandler(applicationName,domain,path,repositories);
			OwnedVisitor visitor = new OwnedVisitor(irepohandler);
			visitor.process((Package)repo);
			
			// create Repo Code
			GenerateRepoProjectCodeHandler repohandler =new GenerateRepoProjectCodeHandler(applicationName,domain,path);
			visitor = new OwnedVisitor(repohandler);
			visitor.process((Package)repo);
			
			// Creation IUoW
			String iuowPath=path+"\\IRepositories";
			String name="IUnitOfWork.cs";
			IRepositoryProjectTemplate _irepotemplate=new IRepositoryProjectTemplate(applicationName, domain);
			try {
				File csprojFile =new File(iuowPath+"\\"+name);
				csprojFile.createNewFile();
				FileWriter writer = new FileWriter(csprojFile);
				try {
		            writer.write(_irepotemplate.getIUnitOfWork());
		        } 
				finally {
		            // quoiqu'il arrive, on ferme le fichier
		            writer.close();
		        }
	        } 
			catch (Exception e) {
	            System.out.println("Impossible de creer le fichier : "+iuowPath+"/"+name);
	        }
			
			// Creation UoW
			String uowPath=path+"\\Repositories";
			name="UnitOfWork.cs";
			RepositoryProjectTemplate _repoTemplate=new RepositoryProjectTemplate(applicationName, domain);
			try {
				File csprojFile =new File(uowPath+"\\"+name);
				csprojFile.createNewFile();
				FileWriter writer = new FileWriter(csprojFile);
				try {
		            writer.write(_repoTemplate.getUnitOfWork(repositories));
		        } 
				finally {
		            // quoiqu'il arrive, on ferme le fichier
		            writer.close();
		        }
	        } 
			catch (Exception e) {
	            System.out.println("Impossible de creer le fichier : "+uowPath+"/"+name);
	        }
		}
		else
		{
			next = false;
		}
		
		ModelTree service = ((Package)selectedModelPsm).getOwnedElement().stream().filter(e -> PsmStereotypeValidator.IsPsmServicePackage(e)).findFirst().orElse(null);
		if(next && service!=null && service instanceof Package)
		{
			// create IServ Code
			GenerateIServiceProjectCodeHandler iservhandler =new GenerateIServiceProjectCodeHandler(applicationName,domain,path,services);
			OwnedVisitor visitor = new OwnedVisitor(iservhandler);
			visitor.process((Package)service);
			
			// create Serv Code
			GenerateServiceProjectCodeHandler servhandler =new GenerateServiceProjectCodeHandler(applicationName,domain,path);
			visitor = new OwnedVisitor(servhandler);
			visitor.process((Package)service);
		}
		else
		{
			next = false;
		}
		
		// create Webapi\\Controllers Code
		ModelTree webapi = ((Package)selectedModelPsm).getOwnedElement().stream().filter(e -> PsmStereotypeValidator.IsPsmWebApiPackage(e)).findFirst().orElse(null);
		if(next && webapi!=null && webapi instanceof Package)
		{
			GenerateWebapiProjectCodeHandler apihandler =new GenerateWebapiProjectCodeHandler(applicationName,domain,path,services);
			OwnedVisitor visitor = new OwnedVisitor(apihandler);
			visitor.process((Package)webapi);
		}
		else
		{
			next = false;
		}
		
	}
}
