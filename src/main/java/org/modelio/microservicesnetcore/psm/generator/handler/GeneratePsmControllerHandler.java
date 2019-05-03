package org.modelio.microservicesnetcore.psm.generator.handler;

import java.util.Stack;

import org.modelio.api.modelio.model.IModelingSession;
import org.modelio.api.module.IModule;
import org.modelio.metamodel.uml.infrastructure.ModelElement;
import org.modelio.metamodel.uml.statik.Classifier;
import org.modelio.metamodel.uml.statik.AggregationKind;
import org.modelio.metamodel.uml.statik.AssociationEnd;
import org.modelio.metamodel.uml.statik.Class;
import org.modelio.metamodel.uml.statik.Package;
import org.modelio.microservicesnetcore.helper.PimPsmMapper;
import org.modelio.microservicesnetcore.helper.PimStereotypeValidator;
import org.modelio.microservicesnetcore.helper.PsmBuilder;
import org.modelio.microservicesnetcore.helper.PsmStereotypeValidator;
import org.modelio.microservicesnetcore.helper.PsmControllerBuilder;
import org.modelio.modeliotools.treevisitor.HandlerAdapter;


public class GeneratePsmControllerHandler extends HandlerAdapter {
	private Stack<Object> _ctx=new Stack<Object>();
	private IModelingSession _session;
	private boolean isComposition = false;
	
	public GeneratePsmControllerHandler(IModule module,Package psmMicroservice)
	{
		_session = module.getModuleContext().getModelingSession();
		ModelElement psmMicroserviceService=null;
		for(ModelElement child : psmMicroservice.getOwnedElement())
		{
			if(PsmStereotypeValidator.IsPsmWebApiPackage(child))
			{
				psmMicroserviceService=child;
				break;
			}
		}
		if(psmMicroserviceService==null)
		{
			psmMicroserviceService = PsmBuilder.CreatePsmMicroserviceController(_session, psmMicroservice);
		}
		_ctx.push(psmMicroserviceService);
	}
	// ==========================================
	// Begin
	
	@Override
	protected void beginVisitingPackage(Package visited) 
	{
		if(!PimStereotypeValidator.isMicroservice(visited))
		{
			ModelElement psmElt = PimPsmMapper.GetPsmControllerFromPim(visited);
		
			if(psmElt==null)
			{
				psmElt = PsmControllerBuilder.CreatePsmGenericPackage(_session,visited,(Package)_ctx.peek());
			}
			_ctx.push(psmElt);
		}
	}
	
	@Override
	protected void beginVisitingClassifier(Classifier visited) 
	{
		for(AssociationEnd target : visited.getTargetingEnd())
		{
			if(target.getAggregation()== AggregationKind.KINDISCOMPOSITION)
			{
				isComposition = true;
			}
		}
		if(visited instanceof Class && !isComposition)
		{
			Classifier psmElt = (Classifier)PimPsmMapper.GetPsmControllerFromPim(visited);
			if (psmElt==null) {
				psmElt = PsmControllerBuilder.createController( _session,visited, (Package)_ctx.peek());
			}
			_ctx.push(psmElt);
		}
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
		if(visited instanceof Class && !isComposition)
		{
			_ctx.pop();
		}
		isComposition=false;
	}
	
}
