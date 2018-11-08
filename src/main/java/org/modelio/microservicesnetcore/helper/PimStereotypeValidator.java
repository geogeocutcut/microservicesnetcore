package org.modelio.microservicesnetcore.helper;

import org.modelio.metamodel.uml.infrastructure.ModelElement;
import org.modelio.metamodel.uml.statik.Attribute;
import org.modelio.metamodel.uml.statik.Package;
import org.modelio.microservicesnetcore.api.ModuleConstants;
import org.modelio.microservicesnetcore.api.ModuleStereotype;

public class PimStereotypeValidator {

	public static boolean IsPim(ModelElement e) {
		return e.isStereotyped(ModuleConstants.MODULE_NAME, ModuleStereotype.STEREO_PIM);
	}

	public static boolean isMicroserviceModel(Package e) {
		return e.isStereotyped(ModuleConstants.MODULE_NAME, ModuleStereotype.STEREO_PIM_MODEL);
	}
	
	public static boolean isMicroserviceApi(Package e) {
		return e.isStereotyped(ModuleConstants.MODULE_NAME, ModuleStereotype.STEREO_PIM_API);
	}

	public static boolean isMicroservice(Package e) {
		// TODO Auto-generated method stub
		return e.isStereotyped(ModuleConstants.MODULE_NAME, ModuleStereotype.STEREO_PIM_MICROSERVICE);
	}

	public static boolean isIdAttribute(Attribute attr) {
		// TODO Auto-generated method stub
		return e.isStereotyped(ModuleConstants.MODULE_NAME, ModuleStereotype.STEREO_PIM_IDATTRIBUTE);
	}
}
