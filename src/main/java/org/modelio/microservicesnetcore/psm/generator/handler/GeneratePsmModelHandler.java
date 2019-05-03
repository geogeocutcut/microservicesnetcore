package org.modelio.microservicesnetcore.psm.generator.handler;

import java.util.Stack;

import org.modelio.api.modelio.model.IModelingSession;
import org.modelio.api.module.IModule;
import org.modelio.metamodel.uml.infrastructure.ModelElement;
import org.modelio.metamodel.uml.statik.Attribute;
import org.modelio.metamodel.uml.statik.Class;
import org.modelio.metamodel.uml.statik.Classifier;
import org.modelio.metamodel.uml.statik.Enumeration;
import org.modelio.metamodel.uml.statik.Package;
import org.modelio.microservicesnetcore.helper.PimPsmMapper;
import org.modelio.microservicesnetcore.helper.PimStereotypeValidator;
import org.modelio.microservicesnetcore.helper.PsmBuilder;
import org.modelio.microservicesnetcore.helper.PsmModelBuilder;
import org.modelio.microservicesnetcore.helper.PsmStereotypeValidator;
import org.modelio.modeliotools.treevisitor.HandlerAdapter;


public class GeneratePsmModelHandler extends HandlerAdapter {
	private Stack<Object> _ctx= new Stack<Object>();
	private IModelingSession _session;
	
	public GeneratePsmModelHandler(IModule module,Package psmMicroservice)
	{
		_session = module.getModuleContext().getModelingSession();
		ModelElement psmMicroserviceModel=null;
		for(ModelElement child : psmMicroservice.getOwnedElement())
		{
			if(PsmStereotypeValidator.IsPsmModelPackage(child))
			{
				psmMicroserviceModel=child;
				break;
			}
		}
		if(psmMicroserviceModel==null)
		{
			psmMicroserviceModel = PsmBuilder.CreatePsmMicroserviceModel(_session, psmMicroservice);
		}
		_ctx.push(psmMicroserviceModel);
	}
	// ==========================================
	// Begin
	
	@Override
	protected void beginVisitingPackage(Package visited) 
	{
		if(!PimStereotypeValidator.isMicroservice(visited))
		{
			ModelElement psmElt = PimPsmMapper.GetPsmModelFromPim(visited);
			if(psmElt==null)
			{
				psmElt = PsmModelBuilder.CreatePsmGenericPackage(_session,visited,(Package)_ctx.peek());
			}
			_ctx.push(psmElt);
		}
	}
	
	@Override
	protected void beginVisitingClassifier(Classifier visited) 
	{
		Classifier psmElt = (Classifier)PimPsmMapper.GetPsmModelFromPim(visited);
		if(visited instanceof Enumeration)
		{
			if (psmElt==null) {
				psmElt = PsmModelBuilder.createEnumModel( _session,(Enumeration)visited, (Package)_ctx.peek());
			}
		}
		else
		{
			if (psmElt==null) {
				psmElt = PsmModelBuilder.createClassModel( _session,visited, (Package)_ctx.peek());
			}
		}
		_ctx.push(psmElt);
	}
	
	@Override
	protected void beginVisitingAttribute(Attribute visited) 
	{
		Attribute newModelElement = (Attribute)PimPsmMapper.GetPsmModelFromPim(visited);
		if (newModelElement==null){
			newModelElement = PsmModelBuilder.createAttribute(_session,visited, (Classifier)_ctx.peek());
		}
		_ctx.push(newModelElement);
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
	
	@Override
	protected void endVisitingAttribute(Attribute visited) {
		// TODO Auto-generated method stub
		_ctx.pop();
	}
}
