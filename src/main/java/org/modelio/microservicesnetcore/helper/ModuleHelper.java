package org.modelio.microservicesnetcore.helper;

import org.modelio.api.modelio.model.IModelingSession;
import org.modelio.metamodel.uml.infrastructure.ModelElement;
import org.modelio.vcore.smkernel.mapi.MObject;

public class ModuleHelper {

	public static ModelElement getPimPackage(IModelingSession session)
	{
		if(session.getModel().getModelRoots().size()>0)
		{
			ModelElement root =(ModelElement)session.getModel().getModelRoots().get(0);
			for(MObject e : root.getCompositionChildren())
			{
				if(PimStereotypeValidator.IsPim((ModelElement)e))
				{
					return (ModelElement)e;
				}
			}
		}
		return null;
	}

	public static ModelElement getPsmPackage(IModelingSession session) {
		if(session.getModel().getModelRoots().size()>0)
		{
			ModelElement root =(ModelElement)session.getModel().getModelRoots().get(0);
			for(MObject e : root.getCompositionChildren())
			{
				if(PsmStereotypeValidator.IsPsmPackage((ModelElement)e))
				{
					return (ModelElement)e;
				}
			}
		}
		return null;
	}
}
