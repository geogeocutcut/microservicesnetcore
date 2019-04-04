package org.modelio.microservicesnetcore.psm.generator.orchestrator;

import org.modelio.api.modelio.model.IModelingSession;
import org.modelio.api.module.IModule;
import org.modelio.metamodel.uml.infrastructure.ModelElement;
import org.modelio.modeliotools.treevisitor.OwnedVisitor;
import org.modelio.vcore.smkernel.mapi.MObject;
import org.modelio.metamodel.uml.statik.Package;
import org.modelio.microservicesnetcore.helper.ModuleHelper;
import org.modelio.microservicesnetcore.helper.PimPsmMapper;
import org.modelio.microservicesnetcore.helper.PsmBuilder;
import org.modelio.microservicesnetcore.psm.generator.handler.GeneratePsmIServiceHandler;

public class GeneratePsmIServiceOrchestrator {

	private ModelElement _umlPimPackage = null;
	private ModelElement _umlPsmPackage = null;
	private IModule _module;
	private IModelingSession _session;
	
	public GeneratePsmIServiceOrchestrator(IModule module)
	{

		this._module = module;
		this._session  =module.getModuleContext().getModelingSession();
		_umlPimPackage=ModuleHelper.getPimPackage(module.getModuleContext().getModelingSession());
		_umlPsmPackage=ModuleHelper.getPsmPackage(module.getModuleContext().getModelingSession());
		
	}
	
	public void Execute(MObject selectedPimMicroservice) 
	{
		if(_umlPimPackage!=null)
		{
			// 1 create Psm Package if not exist
			_umlPsmPackage= PimPsmMapper.GetPsmFromPim(_umlPimPackage);
			if(_umlPsmPackage==null)
			{
				_umlPsmPackage= PsmBuilder.CreatePsmPackage(_session,(Package)_umlPimPackage);
				
			}
			
			// 2 create Psm Microservice if not exist
			ModelElement psmMicroservice= PimPsmMapper.GetPsmFromPim((ModelElement)selectedPimMicroservice);
			if(psmMicroservice==null)
			{
				psmMicroservice= PsmBuilder.CreatePsmMicroservice(_session,(Package)selectedPimMicroservice,_umlPsmPackage);
				
			}
			
			// 3 create Psm Microservice Repository
			GeneratePsmIServiceHandler handler =new GeneratePsmIServiceHandler(_module, (Package)psmMicroservice);
			OwnedVisitor visitor = new OwnedVisitor(handler);
			visitor.process((Package)selectedPimMicroservice);
			
		}
	}
}
