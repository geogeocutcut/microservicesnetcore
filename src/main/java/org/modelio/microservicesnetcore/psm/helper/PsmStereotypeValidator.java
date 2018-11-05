package org.modelio.microservicesnetcore.psm.helper;

import org.modelio.metamodel.uml.infrastructure.ModelElement;
import org.modelio.microservicesnetcore.api.ModuleConstants;
import org.modelio.microservicesnetcore.api.ModuleStereotype;

public class PsmStereotypeValidator {

	public static boolean IsPsm(ModelElement e) 
	{
		return e.isStereotyped(ModuleConstants.MODULE_NAME, ModuleStereotype.STEREO_PSM);
	}
	
	
}
