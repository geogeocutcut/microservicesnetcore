package org.modelio.microservicesnetcore.psm.generator.handler;

import java.util.Stack;

import org.modelio.api.modelio.model.IModelingSession;
import org.modelio.api.module.IModule;
import org.modelio.api.module.context.log.ILogService;
import org.modelio.metamodel.uml.infrastructure.ModelElement;
import org.modelio.metamodel.uml.statik.Classifier;
import org.modelio.metamodel.uml.statik.Package;
import org.modelio.microservicesnetcore.helper.PimStereotypeValidator;
import org.modelio.microservicesnetcore.helper.PsmModelBuilder;
import org.modelio.microservicesnetcore.psm.helper.PimPsmMapper;
import org.modelio.modeliotools.treevisitor.HandlerAdapter;
import org.modelio.modeliotools.treevisitor.OwnedVisitor;


public class GeneratePsmVersionHandler extends HandlerAdapter {
	private IModelingSession _session;
	private IModule _module;
	public GeneratePsmVersionHandler(IModule module)
	{
		_module=module;
		_session = module.getModuleContext().getModelingSession();
	}
	
	// ==========================================
	// Begin
	
	@Override
	protected void beginVisitingPackage(Package visited) 
	{
		
		ModelElement psmElt = PimPsmMapper.GetPsmFromPim(visited);
		if(psmElt==null)
		{
			ModelElement psmOwner = PimPsmMapper.GetPsmFromPim(visited.getOwner());
			if(psmOwner!=null)
			{
				if(PimStereotypeValidator.isMicroserviceVersion(visited))
				{
					psmElt = PsmModelBuilder.CreatePsmMicroserviceVersion(_session,visited,psmOwner);
				}
				else if(PimStereotypeValidator.isMicroserviceModel(visited))
				{
					// Visitor model package
					GeneratePsmModelHandler ownerHandler = new GeneratePsmModelHandler(this._module);
					OwnedVisitor ownedVisitor = new OwnedVisitor(ownerHandler);
					ownedVisitor.process(visited);
					

					GeneratePsmModelDetailHandler ownerDetailHandler = new GeneratePsmModelDetailHandler(this._module);
					ownedVisitor = new OwnedVisitor(ownerDetailHandler);
					ownedVisitor.process(visited);
					
				}

				else if(PimStereotypeValidator.isMicroserviceApi(visited))
				{
					// Visitor repository interface package
//					GeneratePsmModelHandler ownerRepoInterfaceHandler = new GeneratePsmModelHandler(this._module);
//					OwnedVisitor ownedVisitor = new OwnedVisitor(ownerRepoInterfaceHandler);
//					ownedVisitor.process(visited);
					
					// Visitor repository impl package
//					GeneratePsmModelHandler ownerRepoImplHandler = new GeneratePsmModelHandler(this._module);
//					ownedVisitor = new OwnedVisitor(ownerRepoImplHandler);
//					ownedVisitor.process(visited);

					// Visitor service interface package
//					GeneratePsmModelHandler ownerServiceInterfaceHandler = new GeneratePsmModelHandler(this._module);
//					ownedVisitor = new OwnedVisitor(ownerServiceInterfaceHandler);
//					ownedVisitor.process(visited);
					
					// Visitor service impl package
//					GeneratePsmModelHandler ownerServiceImplHandler = new GeneratePsmModelHandler(this._module);
//					ownedVisitor = new OwnedVisitor(ownerServiceImplHandler);
//					ownedVisitor.process(visited);

					// Visitor webapi package
//					GeneratePsmModelHandler ownerWebapiHandler = new GeneratePsmModelHandler(this._module);
//					ownedVisitor = new OwnedVisitor(ownerWebapiHandler);
//					ownedVisitor.process(visited);
					
				}
			}
		}
	}
	
	// =============================================
	// End
	
}
