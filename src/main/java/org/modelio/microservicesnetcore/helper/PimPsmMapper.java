package org.modelio.microservicesnetcore.helper;

import org.modelio.metamodel.uml.infrastructure.Dependency;
import org.modelio.metamodel.uml.infrastructure.ModelElement;
import org.modelio.microservicesnetcore.api.ModuleConstants;
import org.modelio.microservicesnetcore.api.ModuleStereotype;

public class PimPsmMapper {


	public static ModelElement GetPsmModelFromPim(ModelElement from) {
		// TODO Auto-generated method stub
		ModelElement result = null;
		for (Dependency dep : from.getImpactedDependency()) {
			if (dep.isStereotyped(ModuleConstants.MODULE_NAME,  (ModuleStereotype.STEREO_PSMModelDependency)))
			{		
				result = (ModelElement)dep.getImpacted();
				break;
			}
        }
		return result;
	}
	
	public static ModelElement GetPimFromPsmModel(ModelElement from) {
		// TODO Auto-generated method stub
		ModelElement result = null;
		for (Dependency dep : from.getDependsOnDependency()) {
			if (dep.isStereotyped(ModuleConstants.MODULE_NAME,  (ModuleStereotype.STEREO_PSMModelDependency)))
			{		
				result = (ModelElement)dep.getDependsOn();
				break;
			}
		}
		return result;
	}
	

	public static ModelElement GetPsmIRepositoryFromPim(ModelElement from) {
		// TODO Auto-generated method stub
		ModelElement result = null;
		for (Dependency dep : from.getImpactedDependency()) {
			if (dep.isStereotyped(ModuleConstants.MODULE_NAME,  (ModuleStereotype.STEREO_PSMIRepositoryDependency)))
			{		
				result = (ModelElement)dep.getImpacted();
				break;
			}
        }
		return result;
	}
	
	public static ModelElement GetPimFromPsmIRepository(ModelElement from) {
		// TODO Auto-generated method stub
		ModelElement result = null;
		for (Dependency dep : from.getDependsOnDependency()) {
			if (dep.isStereotyped(ModuleConstants.MODULE_NAME,  (ModuleStereotype.STEREO_PSMIRepositoryDependency)))
			{		
				result = (ModelElement)dep.getDependsOn();
				break;
			}
		}
		return result;
	}
	
	public static ModelElement GetPsmRepositoryFromPim(ModelElement from) {
		// TODO Auto-generated method stub
		ModelElement result = null;
		for (Dependency dep : from.getImpactedDependency()) {
			if (dep.isStereotyped(ModuleConstants.MODULE_NAME,  (ModuleStereotype.STEREO_PSMRepositoryDependency)))
			{		
				result = (ModelElement)dep.getImpacted();
				break;
			}
        }
		return result;
	}
	
	public static ModelElement GetPimFromPsmRepository(ModelElement from) {
		// TODO Auto-generated method stub
		ModelElement result = null;
		for (Dependency dep : from.getDependsOnDependency()) {
			if (dep.isStereotyped(ModuleConstants.MODULE_NAME,  (ModuleStereotype.STEREO_PSMRepositoryDependency)))
			{		
				result = (ModelElement)dep.getDependsOn();
				break;
			}
		}
		return result;
	}

	public static ModelElement GetPsmIServiceFromPim(ModelElement from) {
		// TODO Auto-generated method stub
		ModelElement result = null;
		for (Dependency dep : from.getImpactedDependency()) {
			if (dep.isStereotyped(ModuleConstants.MODULE_NAME,  (ModuleStereotype.STEREO_PSMIServiceDependency)))
			{		
				result = (ModelElement)dep.getImpacted();
				break;
			}
        }
		return result;
	}
	
	public static ModelElement GetPimFromPsmIService(ModelElement from) {
		// TODO Auto-generated method stub
		ModelElement result = null;
		for (Dependency dep : from.getDependsOnDependency()) {
			if (dep.isStereotyped(ModuleConstants.MODULE_NAME,  (ModuleStereotype.STEREO_PSMIServiceDependency)))
			{		
				result = (ModelElement)dep.getDependsOn();
				break;
			}
		}
		return result;
	}
	
	public static ModelElement GetPsmServiceFromPim(ModelElement from) {
		// TODO Auto-generated method stub
		ModelElement result = null;
		for (Dependency dep : from.getImpactedDependency()) {
			if (dep.isStereotyped(ModuleConstants.MODULE_NAME,  (ModuleStereotype.STEREO_PSMServiceDependency)))
			{		
				result = (ModelElement)dep.getImpacted();
				break;
			}
        }
		return result;
	}
	
	public static ModelElement GetPimFromPsmService(ModelElement from) {
		// TODO Auto-generated method stub
		ModelElement result = null;
		for (Dependency dep : from.getDependsOnDependency()) {
			if (dep.isStereotyped(ModuleConstants.MODULE_NAME,  (ModuleStereotype.STEREO_PSMServiceDependency)))
			{		
				result = (ModelElement)dep.getDependsOn();
				break;
			}
		}
		return result;
	}
	
	public static ModelElement GetPsmApiFromPim(ModelElement from) {
		// TODO Auto-generated method stub
		ModelElement result = null;
		for (Dependency dep : from.getImpactedDependency()) {
			if (dep.isStereotyped(ModuleConstants.MODULE_NAME,  (ModuleStereotype.STEREO_PSMApiDependency)))
			{		
				result = (ModelElement)dep.getImpacted();
				break;
			}
        }
		return result;
	}
	
	public static ModelElement GetPimFromPsmApi(ModelElement from) {
		// TODO Auto-generated method stub
		ModelElement result = null;
		for (Dependency dep : from.getDependsOnDependency()) {
			if (dep.isStereotyped(ModuleConstants.MODULE_NAME,  (ModuleStereotype.STEREO_PSMApiDependency)))
			{		
				result = (ModelElement)dep.getDependsOn();
				break;
			}
		}
		return result;
	}
}
