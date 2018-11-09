package org.modelio.microservicesnetcore.helper;

import org.modelio.metamodel.uml.infrastructure.ModelElement;
import org.modelio.metamodel.uml.statik.Attribute;
import org.modelio.microservicesnetcore.api.ModuleConstants;
import org.modelio.microservicesnetcore.api.ModuleStereotype;

public class PsmStereotypeValidator {

	public static boolean IsPsmPackage(ModelElement e) 
	{
		return e.isStereotyped(ModuleConstants.MODULE_NAME, ModuleStereotype.STEREO_PSM);
	}

	public static boolean IsPsmModelPackage(ModelElement e) 
	{
		return e.isStereotyped(ModuleConstants.MODULE_NAME, ModuleStereotype.STEREO_PSM_MODEL);
	}
	
	public static boolean IsPsmIRepositoryPackage(ModelElement e) 
	{
		return e.isStereotyped(ModuleConstants.MODULE_NAME, ModuleStereotype.STEREO_PSM_IREPOSITORY);
	}
	
	public static boolean IsPsmRepositoryPackage(ModelElement e) 
	{
		return e.isStereotyped(ModuleConstants.MODULE_NAME, ModuleStereotype.STEREO_PSM_REPOSITORY);
	}
	
	public static boolean IsPsmIServicePackage(ModelElement e) 
	{
		return e.isStereotyped(ModuleConstants.MODULE_NAME, ModuleStereotype.STEREO_PSM_ISERVICE);
	}
	
	public static boolean IsPsmServicePackage(ModelElement e) 
	{
		return e.isStereotyped(ModuleConstants.MODULE_NAME, ModuleStereotype.STEREO_PSM_SERVICE);
	}

	public static boolean IsPsmWebApiPackage(ModelElement e) 
	{
		return e.isStereotyped(ModuleConstants.MODULE_NAME, ModuleStereotype.STEREO_PSM_API);
	}
	
	public static boolean isIdAttribute(Attribute e) {
		// TODO Auto-generated method stub
		return e.isStereotyped(ModuleConstants.MODULE_NAME, ModuleStereotype.STEREO_PSM_IDATTRIBUTE);
	}
	
	
}
