package org.modelio.microservicesnetcore.psm.generator.handler;

import java.util.Stack;

import org.modelio.api.modelio.model.IModelingSession;
import org.modelio.api.module.IModule;
import org.modelio.metamodel.uml.infrastructure.ModelElement;
import org.modelio.metamodel.uml.statik.Attribute;
import org.modelio.metamodel.uml.statik.Classifier;
import org.modelio.metamodel.uml.statik.Package;
import org.modelio.microservicesnetcore.helper.PimPsmMapper;
import org.modelio.microservicesnetcore.helper.PsmBuilder;
import org.modelio.microservicesnetcore.helper.PsmServiceBuilder;
import org.modelio.microservicesnetcore.helper.PsmStereotypeValidator;
import org.modelio.modeliotools.treevisitor.HandlerAdapter;


public class GeneratePsmWebApiHandler extends HandlerAdapter {
	private Stack<Object> _ctx;
	private IModelingSession _session;
	
	public GeneratePsmWebApiHandler(IModule module,Package psmMicroservice)
	{
		_session = module.getModuleContext().getModelingSession();
		ModelElement psmMicroserviceService=null;
		for(ModelElement child : psmMicroservice.getOwnedElement())
		{
			if(PsmStereotypeValidator.IsPsmServicePackage(child))
			{
				psmMicroserviceService=child;
				break;
			}
		}
		if(psmMicroserviceService==null)
		{
			psmMicroserviceService = PsmBuilder.CreatePsmMicroserviceWebApi(_session, psmMicroservice);
		}
		_ctx.push(psmMicroserviceService);
	}
	// ==========================================
	// Begin
	
	@Override
	protected void beginVisitingPackage(Package visited) 
	{
		ModelElement psmElt = PimPsmMapper.GetPsmServiceFromPim(visited);
		if(psmElt==null)
		{
			psmElt = PsmServiceBuilder.CreatePsmGenericPackage(_session,visited,(Package)_ctx.peek());
		}
		_ctx.push(psmElt);
	}
	
	@Override
	protected void beginVisitingClassifier(Classifier visited) 
	{
		Classifier psmElt = (Classifier)PimPsmMapper.GetPsmServiceFromPim(visited);
		if (psmElt==null) {
			psmElt = PsmServiceBuilder.createService( _session,visited, (Package)_ctx.peek());
		}
		_ctx.push(psmElt);
	}
	
	// =============================================
	// End
	
	@Override
	protected void endVisitingPackage(Package visited) 
	{
		_ctx.pop();
	}
	
	@Override
	protected void endVisitingClassifier(Classifier visited) {
		// TODO Auto-generated method stub
		_ctx.pop();
	}
	
}
