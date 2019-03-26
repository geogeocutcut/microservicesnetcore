package org.modelio.microservicesnetcore.helper;

import org.modelio.api.modelio.model.IModelingSession;
import org.modelio.api.modelio.model.IUMLTypes;
import org.modelio.api.modelio.model.IUmlModel;
import org.modelio.api.module.IModule;
import org.modelio.metamodel.uml.infrastructure.ModelElement;
import org.modelio.metamodel.uml.statik.GeneralClass;
import org.modelio.microservicesnetcore.impl.MicroserviceDotnetCoreModule;
import org.modelio.vcore.smkernel.mapi.MObject;

public class ModuleHelper {

	public static ModelElement getPimPackage(IModelingSession session)
	{
		if(session.getModel().getModelRoots().size()>0)
		{
			for(MObject root : session.getModel().getModelRoots())
			{
				for(MObject e : root.getCompositionChildren())
				{
					if(PimStereotypeValidator.IsPim((ModelElement)e))
					{
						return (ModelElement)e;
					}
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
	
	public static String getNetTypeFromUmlType(GeneralClass type) {

		IModule module = MicroserviceDotnetCoreModule.getInstance();
		IModelingSession session = module.getModuleContext().getModelingSession();
		IUmlModel model = session.getModel();
		IUMLTypes _umlType = model.getUmlTypes();
		
		String result = null;
		if (type.getUuid().equals(_umlType.getSTRING().getUuid()))
			result = "string";
		else if (type.getUuid().equals(_umlType.getDOUBLE().getUuid()))
			result = "double";
		else if (type.getUuid().equals(_umlType.getINTEGER().getUuid()))
			result = "int";
		else if (type.getUuid().equals(_umlType.getBOOLEAN().getUuid()))
			result = "bool";
		else if (type.getUuid().equals(_umlType.getDATE().getUuid()))
			result = "DateTime";
		else
			result = type.getName();
		
		
		return result;
	}
}
