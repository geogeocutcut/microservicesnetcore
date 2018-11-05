package org.modelio.microservicesnetcore.psm.generator;

import java.util.Stack;

import org.modelio.api.modelio.model.IModelingSession;
import org.modelio.api.module.IModule;
import org.modelio.api.module.context.log.ILogService;
import org.modelio.metamodel.uml.infrastructure.ModelElement;
import org.modelio.metamodel.uml.statik.Attribute;
import org.modelio.metamodel.uml.statik.Classifier;
import org.modelio.metamodel.uml.statik.Package;
import org.modelio.microservicesnetcore.psm.helper.PimPsmMapper;
import org.modelio.microservicesnetcore.psm.helper.PimStereotypeValidator;
import org.modelio.microservicesnetcore.psm.helper.PsmBuilder;
import org.modelio.modeliotools.treevisitor.HandlerAdapter;


public class GeneratePsmModelHandler extends HandlerAdapter {
	private Stack<Object> _ctx;
	private IModelingSession _session;
	private ILogService _logService;
	private boolean _isInPackageModel=false;
	
	public GeneratePsmModelHandler(IModule module)
	{
		_session = module.getModuleContext().getModelingSession();
		_logService = module.getModuleContext().getLogService();
	}
	
	// ==========================================
	// Begin
	
	@Override
	protected void beginVisitingPackage(Package visited) 
	{
		
		if( _isInPackageModel || PimStereotypeValidator.isMicroserviceModel(visited))
		{
			ModelElement psmElt = PimPsmMapper.GetPsmFromPim(visited);
			if(psmElt==null)
			{
				ModelElement psmOwner = PimPsmMapper.GetPsmFromPim(visited.getOwner());
				if(psmOwner!=null)
				{
					if(PimStereotypeValidator.isMicroserviceModel(visited))
					{
						_isInPackageModel =true;
						psmElt = PsmBuilder.CreatePsmMicroserviceModel(_session,visited,psmOwner);
					}
					else
					{
						psmElt = PsmBuilder.CreatePsmGenericPackage(_session,visited,psmOwner);
					}
				}
			}
			if(psmElt==null)
			{
				_logService.error("Generate Applicative Architecture Domain Version");
			};
			_ctx.push(psmElt);
		}
	}
	
	
	protected void beginVisitingClassifier(Classifier visited) {
		if( _isInPackageModel )
		{
			Classifier psmElt = (Classifier)PimPsmMapper.GetPsmFromPim(visited);
			if (psmElt==null) {
				psmElt = PsmBuilder.createClassModel( _session,visited, (Package)_ctx.peek());
			}
			_ctx.push(psmElt);
		}
	}
	
	@Override
	protected void beginVisitingAttribute(Attribute visited) 
	{
		Attribute newModelElement = (Attribute)PimPsmMapper.GetPsmFromPim(visited);
		if (newModelElement==null){
			newModelElement = PsmBuilder.createAttribute(visited, (Classifier)_ctx.peek(), _session);
		}
		_ctx.push(newModelElement);
	}
	
	// =============================================
	// End
	
	@Override
	protected void endVisitingPackage(Package visited) 
	{
		if(PimStereotypeValidator.isMicroserviceModel(visited))
		{
			_isInPackageModel = false;
		}
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
