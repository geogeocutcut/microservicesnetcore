package org.modelio.microservicesnetcore.helper;

import org.modelio.api.modelio.model.IModelingSession;
import org.modelio.api.modelio.model.IUmlModel;
import org.modelio.metamodel.mda.Project;
import org.modelio.metamodel.uml.infrastructure.Dependency;
import org.modelio.metamodel.uml.infrastructure.ModelElement;
import org.modelio.metamodel.uml.infrastructure.Stereotype;
import org.modelio.metamodel.uml.statik.NameSpace;
import org.modelio.metamodel.uml.statik.Package;
import org.modelio.microservicesnetcore.api.ModuleConstants;
import org.modelio.microservicesnetcore.api.ModuleStereotype;

public class PsmBuilder {
	public static void CreatePimDependency(IModelingSession session,ModelElement pimElt,ModelElement psmElt)
	{
		// Stereotype PimPsmDependency
		Stereotype pimImpactStereotype = ModuleStereotype.GetStereotype(session, Dependency.class, ModuleStereotype.STEREO_PSMDependency);

		IUmlModel model= session.getModel();
		model.createDependency(psmElt, pimElt,pimImpactStereotype);
	}

	public static ModelElement CreatePsmPackage(IModelingSession session, ModelElement umlPimPackage) {
		IUmlModel model= session.getModel();
		Project root = (Project)model.getModelRoots().get(0);
		
		String name = ModuleConstants.getPSMName(umlPimPackage.getName());
		// Stereotype PSM
		Stereotype psmStereo = ModuleStereotype.GetStereotype(session, Package.class, ModuleStereotype.STEREO_PSM);
		Package psmElt = model.createPackage();
		psmElt.setName(name);
		psmElt.getExtension().add(psmStereo);
		root.getModel().add(psmElt);
		
		
		// Stereotype PimDependency
		CreatePimDependency(session ,umlPimPackage,psmElt);
		
		return psmElt;
	}
	
	public static ModelElement CreatePsmMicroservice(IModelingSession session, Package visited, ModelElement psmOwner) 
	{
		ModelElement psmElt = CreatePsmGenericPackage(session,visited,psmOwner);
		
		// Stereotype PSM
		Stereotype psmStereo = ModuleStereotype.GetStereotype(session, Package.class, ModuleStereotype.STEREO_PSM_MICROSERVICE);
		psmElt.getExtension().add(psmStereo);
		
		return psmElt;
	}

	public static ModelElement CreatePsmGenericPackage(IModelingSession session, Package visited, ModelElement psmOwner) 
	{
		IUmlModel model= session.getModel();
		
		Package psmElt = model.createPackage(visited.getName(),(NameSpace)psmOwner);
		
		// Stereotype PimDependency
		CreatePimDependency(session ,visited,psmElt);
		
		return psmElt;
	}

	public static ModelElement CreatePsmMicroserviceModel(IModelingSession session, ModelElement psmOwner) 
	{
		IUmlModel model= session.getModel();
		
		Package psmElt = model.createPackage(ModuleConstants.PSM_ModelPackageName,(NameSpace)psmOwner);
		Stereotype psmStereo = ModuleStereotype.GetStereotype(session, Package.class, ModuleStereotype.STEREO_PSM_MODEL);
		psmElt.getExtension().add(psmStereo);
		
		return psmElt;
	}

	public static ModelElement CreatePsmMicroserviceIRepository(IModelingSession session, ModelElement psmOwner) {
		// TODO Auto-generated method stub
		IUmlModel model= session.getModel();
		
		Package psmElt = model.createPackage(ModuleConstants.PSM_IRepositoryPackageName,(NameSpace)psmOwner);
		Stereotype psmStereo = ModuleStereotype.GetStereotype(session, Package.class, ModuleStereotype.STEREO_PSM_IREPOSITORY);
		psmElt.getExtension().add(psmStereo);
		
		return psmElt;
	}

	public static ModelElement CreatePsmMicroserviceRepository(IModelingSession session, ModelElement psmOwner) {
		// TODO Auto-generated method stub
		IUmlModel model= session.getModel();
		
		Package psmElt = model.createPackage(ModuleConstants.PSM_RepositoryPackageName,(NameSpace)psmOwner);
		Stereotype psmStereo = ModuleStereotype.GetStereotype(session, Package.class, ModuleStereotype.STEREO_PSM_REPOSITORY);
		psmElt.getExtension().add(psmStereo);
		
		return psmElt;
	}

	public static ModelElement CreatePsmMicroserviceIService(IModelingSession session, ModelElement psmOwner) {
		// TODO Auto-generated method stub
		IUmlModel model= session.getModel();
		
		Package psmElt = model.createPackage(ModuleConstants.PSM_IServicePackageName,(NameSpace)psmOwner);
		Stereotype psmStereo = ModuleStereotype.GetStereotype(session, Package.class, ModuleStereotype.STEREO_PSM_ISERVICE);
		psmElt.getExtension().add(psmStereo);
		
		return psmElt;
	}

	public static ModelElement CreatePsmMicroserviceService(IModelingSession session, ModelElement psmOwner) {
		// TODO Auto-generated method stub
		IUmlModel model= session.getModel();
		
		Package psmElt = model.createPackage(ModuleConstants.PSM_ServicePackageName,(NameSpace)psmOwner);
		Stereotype psmStereo = ModuleStereotype.GetStereotype(session, Package.class, ModuleStereotype.STEREO_PSM_SERVICE);
		psmElt.getExtension().add(psmStereo);
		
		return psmElt;
	}
	
	public static ModelElement CreatePsmMicroserviceController(IModelingSession session, ModelElement psmOwner) {
		// TODO Auto-generated method stub
		IUmlModel model= session.getModel();
		
		Package psmElt = model.createPackage(ModuleConstants.PSM_ControllerPackageName,(NameSpace)psmOwner);
		Stereotype psmStereo = ModuleStereotype.GetStereotype(session, Package.class, ModuleStereotype.STEREO_PSM_API);
		psmElt.getExtension().add(psmStereo);
		
		return psmElt;
	}
}
