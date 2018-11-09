package org.modelio.microservicesnetcore.psm.generator.handler;

import org.modelio.api.modelio.model.IModelingSession;
import org.modelio.api.module.IModule;
import org.modelio.metamodel.uml.statik.AssociationEnd;
import org.modelio.metamodel.uml.statik.Classifier;
import org.modelio.metamodel.uml.statik.Package;
import org.modelio.microservicesnetcore.helper.PimPsmMapper;
import org.modelio.microservicesnetcore.helper.PsmModelBuilder;
import org.modelio.modeliotools.treevisitor.HandlerAdapter;

public class GeneratePsmModelDetailHandler extends HandlerAdapter{
	private IModelingSession _session;
	
	public GeneratePsmModelDetailHandler(IModule module)
	{
		_session = module.getModuleContext().getModelingSession();
	}
	@Override
	protected void beginVisitingPackage(Package visited) 
	{
	}
	
	@Override
	protected void beginVisitingClassifier(Classifier visited) 
	{
	}

	
	@Override
	protected void beginVisitingAssociationEnd(AssociationEnd visited) 
	{
		AssociationEnd psmModelAssociationEnd = (AssociationEnd)PimPsmMapper.GetPsmFromPim(visited);
		if(psmModelAssociationEnd == null)
		{
			psmModelAssociationEnd = PsmModelBuilder.createAssociationEnd(_session,visited );
		}
	}


	///////////////////////////////////////////////////////////////////
	@Override
	protected void endVisitingPackage(Package visited) 
	{
	}
	
	@Override
	protected void endVisitingClassifier(Classifier visited) 
	{
	}
}
