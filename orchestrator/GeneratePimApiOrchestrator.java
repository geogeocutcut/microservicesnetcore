package org.modelio.microservicesnetcore.psm.generator.orchestrator;

import org.modelio.api.modelio.model.IModelingSession;
import org.modelio.api.modelio.model.ITransaction;
import org.modelio.api.modelio.model.IUmlModel;
import org.modelio.api.module.IModule;
import org.modelio.api.module.context.log.ILogService;
import org.modelio.metamodel.mda.Project;
import org.modelio.metamodel.uml.infrastructure.ModelElement;
import org.modelio.modeliotools.treevisitor.OwnedVisitor;
import org.modelio.modeliotools.treevisitor.OwnerVisitor;
import org.modelio.vcore.smkernel.mapi.MObject;
import org.modelio.metamodel.uml.statik.Package;
import org.modelio.microservicesnetcore.helper.ModuleHelper;
import org.modelio.microservicesnetcore.psm.generator.handler.GeneratePsmVersionHandler;
import org.modelio.microservicesnetcore.psm.helper.PimPsmMapper;
import org.modelio.microservicesnetcore.psm.helper.PimStereotypeValidator;
import org.modelio.microservicesnetcore.psm.helper.PsmBuilder;

public class GeneratePimApiOrchestrator {

	private IModule _module;
	private IModelingSession _session;
	private ILogService _logService;
	
	public GeneratePimApiOrchestrator(IModule module)
	{
		_logService = module.getModuleContext().getLogService();

		this._module = module;
		this._session  =module.getModuleContext().getModelingSession();
		
	}
	
	public void Execute(Package selectedMicroservice) 
	{
		Package apiPackage =null;
		for(Package child : selectedMicroservice.getOwnedElement(Package.class))
		{
			if(PimStereotypeValidator.isMicroserviceApi(child))
			{
				apiPackage=child;
				break;
			}
		}
		if(apiPackage==null)
		{
			try (ITransaction t = _session.createTransaction("Create PSM Package")) {
				apiPackage=PimBuilder.CreatePimApiPackage(_session,selectedMicroservice);
				t.commit();
			}
		}
		if(_umlPimPackage!=null)
		{
			// 1 create Api Package if not exist
			_umlPsmPackage= PimPsmMapper.GetPsmFromPim(_umlPimPackage);
			if(_umlPsmPackage==null)
			{
				try (ITransaction t = _session.createTransaction("Create PSM Package")) {
					_umlPsmPackage= PsmBuilder.CreatePsmPackage(_session,_umlPimPackage);
					t.commit();
				}
				catch (Exception e) {
					throw e;
				}
			}
			
			// 2 create Microservice if not exist
			
			// 3 Parcours des enfants Microservices
			for(ModelElement child : selectedMicroservice.getOwnedElement())
			{
				// a. Package Model
				if(PimStereotypeValidator.isMicroserviceModel((Package)child))
				{
					GeneratePsmModelOrchestrator psmModelOrchestrator = new GeneratePsmModelOrchestrator(_module);
					psmModelOrchestrator.Execute(child);
				}
				// a. Package Api
				else if(PimStereotypeValidator.isMicroserviceApi((Package)child))
				{
					GeneratePsmRepoInterfaceOrchestrator psmRepoInterfaceOrchestrator = new GeneratePsmRepoInterfaceOrchestrator(_module);
					psmRepoInterfaceOrchestrator.Execute(child);
					
					GeneratePsmRepoImplOrchestrator psmRepoImplOrchestrator = new GeneratePsmRepoImplOrchestrator(_module);
					psmRepoImplOrchestrator.Execute(child);
					
					GeneratePsmServiceInterfaceOrchestrator psmServiceInterfaceOrchestrator = new GeneratePsmServiceInterfaceOrchestrator(_module);
					psmServiceInterfaceOrchestrator.Execute(child);
					
					GeneratePsmServiceImplOrchestrator psmServiceImplOrchestrator = new GeneratePsmServiceImplOrchestrator(_module);
					psmServiceImplOrchestrator.Execute(child);
					
					GeneratePsmWebApiOrchestrator psmWebApiOrchestrator = new GeneratePsmWebApiOrchestrator(_module);
					psmWebApiOrchestrator.Execute(child);
				}
			}
				
			
		}
	}
}
