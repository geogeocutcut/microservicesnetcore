package org.modelio.microservicesnetcore.psm.helper;

import org.modelio.metamodel.uml.infrastructure.ModelElement;
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
}
