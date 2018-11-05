package org.modelio.microservicesnetcore.psm.generator;

import java.util.Stack;

import org.modelio.api.modelio.model.IModelingSession;
import org.modelio.api.module.IModule;
import org.modelio.api.module.context.log.ILogService;
import org.modelio.metamodel.uml.statik.AssociationEnd;
import org.modelio.metamodel.uml.statik.Classifier;
import org.modelio.metamodel.uml.statik.Package;
import org.modelio.microservicesnetcore.psm.helper.PimPsmMapper;
import org.modelio.microservicesnetcore.psm.helper.PsmBuilder;
import org.modelio.modeliotools.treevisitor.HandlerAdapter;

public class GeneratePsmModelDetailHandler extends HandlerAdapter{
	private Stack<Object> _ctx;
	private IModelingSession _session;
	private ILogService _logService;
	
	public GeneratePsmModelDetailHandler(IModule module)
	{
		_session = module.getModuleContext().getModelingSession();
		_logService = module.getModuleContext().getLogService();
	}
	@Override
	protected void beginVisitingPackage(Package visited) 
	{
		Package newModelElement = (Package)PimPsmMapper.GetPsmFromPim(visited);
		_ctx.push(newModelElement);
	}
	
	@Override
	protected void beginVisitingClassifier(Classifier visited) {
		Classifier newModelElement = (Classifier)PimPsmMapper.GetPsmFromPim(visited);
		_ctx.push(newModelElement);
	}

	
	@Override
	protected void beginVisitingAssociationEnd(AssociationEnd visited) {

		AssociationEnd dataModelAssociationEnd = (AssociationEnd)PimPsmMapper.GetPsmFromPim(visited);
		if(dataModelAssociationEnd == null)
		{
			dataModelAssociationEnd = PsmBuilder.createAssociationEnd(visited, _session);
		}
	}


	///////////////////////////////////////////////////////////////////
	@Override
	protected void endVisitingPackage(Package visited) {
		_ctx.pop();
	}
	
	@Override
	protected void endVisitingClassifier(Classifier visited) {
		_ctx.pop();
	}
}
