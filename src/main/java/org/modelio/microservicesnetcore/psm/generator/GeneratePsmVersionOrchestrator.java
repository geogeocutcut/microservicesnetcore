package org.modelio.microservicesnetcore.psm.generator;

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
import org.modelio.microservicesnetcore.psm.helper.PimPsmMapper;
import org.modelio.microservicesnetcore.psm.helper.PsmBuilder;

public class GeneratePsmVersionOrchestrator {

	private ModelElement _umlPimPackage = null;
	private ModelElement _umlPsmPackage = null;
	private IModule _module;
	private IModelingSession _session;
	private ILogService _logService;
	
	public GeneratePsmVersionOrchestrator(IModule module)
	{
		_logService = module.getModuleContext().getLogService();

		this._module = module;
		this._session  =module.getModuleContext().getModelingSession();
		_umlPimPackage=ModuleHelper.getPimPackage(module.getModuleContext().getModelingSession());
		
	}
	
	public void Execute(MObject selectedPimVersionPackage) 
	{
		if(_umlPimPackage!=null)
		{
			// 1 create Psm Package if not exist
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
			// 2 create Parent Package if not exist
			GeneratePsmVersionParentHandler psmParentVersionHandler = new GeneratePsmVersionParentHandler(this._module);
			OwnerVisitor ownerVisitor = new OwnerVisitor(psmParentVersionHandler);
			ownerVisitor.process((Package)selectedPimVersionPackage);
			
			// 3 create recursively the owned package
			// 		call GenerateModelOrchestrator
			//      call GenerateRepoInterfaceOrchestrator
			//		call GenerateRepoImplOrchestrator
			//		call GenerateServiceInterfaceOrchestrator
			//		call GenerateServiceImplOrchestrator
			//		call GenerateWebapiOrchestrator
			
			GeneratePsmVersionHandler psmVersionHandler = new GeneratePsmVersionHandler(this._module);
			OwnedVisitor ownedVisitor = new OwnedVisitor(psmVersionHandler);
			ownedVisitor.process((Package)selectedPimVersionPackage);
		
			
		}
	}
}
