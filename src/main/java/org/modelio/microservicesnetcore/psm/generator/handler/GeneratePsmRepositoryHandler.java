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
import org.modelio.microservicesnetcore.helper.PsmRepositoryBuilder;
import org.modelio.microservicesnetcore.helper.PsmStereotypeValidator;
import org.modelio.modeliotools.treevisitor.HandlerAdapter;


public class GeneratePsmRepositoryHandler extends HandlerAdapter {
	private Stack<Object> _ctx=new Stack<Object>();
	private IModelingSession _session;
	
	public GeneratePsmRepositoryHandler(IModule module,Package psmMicroservice)
	{
		_session = module.getModuleContext().getModelingSession();
		ModelElement psmMicroserviceRepository=null;
		for(ModelElement child : psmMicroservice.getOwnedElement())
		{
			if(PsmStereotypeValidator.IsPsmRepositoryPackage(child))
			{
				psmMicroserviceRepository=child;
				break;
			}
		}
		if(psmMicroserviceRepository==null)
		{
			psmMicroserviceRepository = PsmBuilder.CreatePsmMicroserviceRepository(_session, psmMicroservice);
		}
		_ctx.push(psmMicroserviceRepository);
	}
	// ==========================================
	// Begin
	
	@Override
	protected void beginVisitingPackage(Package visited) 
	{
		if(!PimStereotypeValidator.isMicroservice(visited))
		{
			ModelElement psmElt = PimPsmMapper.GetPsmRepositoryFromPim(visited);
		
			if(psmElt==null)
			{
				psmElt = PsmRepositoryBuilder.CreatePsmGenericPackage(_session,visited,(Package)_ctx.peek());
			}
			_ctx.push(psmElt);
		}
	}
	
	@Override
	protected void beginVisitingClassifier(Classifier visited) 
	{
		Classifier psmElt = (Classifier)PimPsmMapper.GetPsmRepositoryFromPim(visited);
		if (psmElt==null) {
			psmElt = PsmRepositoryBuilder.createRepository( _session,visited, (Package)_ctx.peek());
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
