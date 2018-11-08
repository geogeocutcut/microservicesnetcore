package org.modelio.microservicesnetcore.api;

import org.modelio.api.modelio.meta.IMetamodelService;
import org.modelio.api.modelio.model.IModelingSession;
import org.modelio.api.module.context.IModuleContext;
import org.modelio.metamodel.uml.infrastructure.Stereotype;
import org.modelio.microservicesnetcore.impl.MicroserviceDotnetCoreModule;
import org.modelio.vcore.smkernel.mapi.MClass;
import org.modelio.vcore.smkernel.mapi.MMetamodel;
import org.modelio.vcore.smkernel.mapi.MObject;

public class ModuleStereotype {
	
	private static MicroserviceDotnetCoreModule myModule = MicroserviceDotnetCoreModule.getInstance();

	public static String STEREO_PIMDependency = "PimImpact";
	
	public static String STEREO_PIM = "PIM";
	public static String STEREO_PIM_MICROSERVICE = "PIMMicroservice";
	public static String STEREO_PIM_IDATTRIBUTE = "PIMIdAttribute";
	
	public static String STEREO_PSM = "PSM";
	public static String STEREO_PSM_MICROSERVICE = "PSMMicroservice";
	public static String STEREO_PSM_MODEL = "PSMModelPackage";
	public static String STEREO_PSM_REPOSITORY = "PSMReporsitoryPackage";
	public static String STEREO_PSM_IREPOSITORY = "PSMIReporsitoryPackage";
	public static String STEREO_PSM_SERVICE = "PSMServicePackage";
	public static String STEREO_PSM_ISERVICE = "PSMIServicePackage";
	public static String STEREO_PSM_API = "PSMAPIPackage";
	public static String STEREO_PSM_CONTROLLER = "PSMController";
	public static String STEREO_PSM_IDATTRIBUTE = "PSMIdAttribute";
	
	public static String STEREO_CS_MICROSERVICE = "CsMicroservice";
	public static String STEREO_CS_STANDARD_PROJECT = "CsStandardProject";
	public static String STEREO_CS_WEBAPI_PROJECT = "CsWebApiProject";
	public static String STEREO_CS_PACKAGE = "CsPackage";
	public static String STEREO_CS_PROJECTFOLDER = "CsProjectFolder";
	public static String STEREO_CS_CLASS = "CsClass";
	public static String STEREO_CS_CONTROLLER = "CsController";
	public static String STEREO_CS_ATTRIBUTE = "CsAttribute";
	public static String STEREO_CS_PROPERTY = "CsProperty";
	public static String STEREO_CS_METHOD = "CsMethod";
	public static String STEREO_CS_EXPOSEDMETHOD = "CsExposedOperation";
	
	
	public static Stereotype GetStereotype(IModelingSession session,Class<? extends MObject> type, String name) {
		Stereotype result = null;
		IModuleContext ctx = myModule.getModuleContext();
		IMetamodelService mmService = ctx.getModelioServices().getMetamodelService();
		MMetamodel metamodel = mmService.getMetamodel();
		MClass mClass = metamodel.getMClass(type);
		result = session.getMetamodelExtensions().getStereotype(ModuleConstants.MODULE_NAME,name,mClass);
		return result;
	}


}
