package org.modelio.microservicesnetcore.psm.helper;

import org.modelio.metamodel.uml.infrastructure.Dependency;
import org.modelio.metamodel.uml.infrastructure.ModelElement;
import org.modelio.microservicesnetcore.api.ModuleConstants;
import org.modelio.microservicesnetcore.api.ModuleStereotype;

public class PimPsmMapper {


	public static ModelElement GetPsmFromPim(ModelElement from) {
		// TODO Auto-generated method stub
		ModelElement result = null;
		for (Dependency dep : from.getImpactedDependency()) {
			if (dep.isStereotyped(ModuleConstants.MODULE_NAME,  (ModuleStereotype.STEREO_PIMPSMDependency)))
			{		
				result = (ModelElement)dep.getImpacted();
				break;
			}
        }
		return result;
	}
	
	public static ModelElement GetPimFromPsm(ModelElement from) {
		// TODO Auto-generated method stub
		ModelElement result = null;
		for (Dependency dep : from.getDependsOnDependency()) {
			if (dep.isStereotyped(ModuleConstants.MODULE_NAME,  (ModuleStereotype.STEREO_PIMPSMDependency)))
			{		
				result = (ModelElement)dep.getDependsOn();
				break;
			}
		}
		return result;
	}
}
