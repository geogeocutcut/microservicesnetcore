package org.modelio.microservicesnetcore.psm.generator.handler;

import java.util.Stack;

import org.modelio.api.modelio.model.IModelingSession;
import org.modelio.api.module.IModule;
import org.modelio.metamodel.uml.infrastructure.ModelElement;
import org.modelio.metamodel.uml.statik.Classifier;
import org.modelio.metamodel.uml.statik.Package;
import org.modelio.microservicesnetcore.helper.PimPsmMapper;
import org.modelio.microservicesnetcore.helper.PimStereotypeValidator;
import org.modelio.microservicesnetcore.helper.PsmBuilder;
import org.modelio.microservicesnetcore.helper.PsmIServiceBuilder;
import org.modelio.microservicesnetcore.helper.PsmStereotypeValidator;
import org.modelio.modeliotools.treevisitor.HandlerAdapter;


public class GeneratePsmIServiceHandler extends HandlerAdapter {
	private Stack<Object> _ctx=new Stack<Object>();
	private IModelingSession _session;
	
	public GeneratePsmIServiceHandler(IModule module,Package psmMicroservice)
	{
		_session = module.getModuleContext().getModelingSession();
		ModelElement psmMicroserviceIService=null;
		for(ModelElement child : psmMicroservice.getOwnedElement())
		{
			if(PsmStereotypeValidator.IsPsmIServicePackage(child))
			{
				psmMicroserviceIService=child;
				break;
			}
		}
		if(psmMicroserviceIService==null)
		{
			psmMicroserviceIService = PsmBuilder.CreatePsmMicroserviceIService(_session, psmMicroservice);
		}
		_ctx.push(psmMicroserviceIService);
	}
	// ==========================================
	// Begin
	
	@Override
	protected void beginVisitingPackage(Package visited) 
	{
		if(!PimStereotypeValidator.isMicroservice(visited))
		{
			ModelElement psmElt = PimPsmMapper.GetPsmIServiceFromPim(visited);
			if(psmElt==null)
			{
				psmElt = PsmIServiceBuilder.CreatePsmGenericPackage(_session,visited,(Package)_ctx.peek());
			}
			_ctx.push(psmElt);
		}
	}
	
	@Override
	protected void beginVisitingClassifier(Classifier visited) 
	{
		Classifier psmElt = (Classifier)PimPsmMapper.GetPsmIServiceFromPim(visited);
		if (psmElt==null) {
			psmElt = PsmIServiceBuilder.createIService( _session,visited, (Package)_ctx.peek());
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
