package org.modelio.microservicesnetcore.api;

import org.modelio.api.modelio.meta.IMetamodelService;
import org.modelio.api.modelio.model.IModelingSession;
import org.modelio.api.module.context.IModuleContext;
import org.modelio.metamodel.uml.infrastructure.TagType;
import org.modelio.microservicesnetcore.impl.MicroserviceDotnetCoreModule;
import org.modelio.vcore.smkernel.mapi.MClass;
import org.modelio.vcore.smkernel.mapi.MMetamodel;
import org.modelio.vcore.smkernel.mapi.MObject;

public class ModuleTagType {
	private static MicroserviceDotnetCoreModule myModule = MicroserviceDotnetCoreModule.getInstance();
	
	public static String TAG_NAME="Name";
	
	public static String TAG_ATT_ROUTEPREFIX="RoutePrefix";
	
	public static String TAG_ATT_ROUTE="Route";
	public static String TAG_ATT_HTTPVERB="Method";
	public static String TAG_GENERATEDIRECTORY="GenerateDirectory";
	
	
	public static TagType GetTagType(IModelingSession session,Class<? extends MObject> type, String name) {
		TagType result = null;
		IModuleContext ctx = myModule.getModuleContext();
		IMetamodelService mmService = ctx.getModelioServices().getMetamodelService();
		MMetamodel metamodel = mmService.getMetamodel();
		MClass mClass = metamodel.getMClass(type);
		result = session.getMetamodelExtensions().getTagType(ModuleConstants.MODULE_NAME,name,mClass);
		return result;
	}
}
