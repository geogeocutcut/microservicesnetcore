package org.modelio.microservicesnetcore.helper;

import org.modelio.api.modelio.model.IModelingSession;
import org.modelio.api.modelio.model.IUmlModel;
import org.modelio.metamodel.mda.Project;
import org.modelio.metamodel.uml.infrastructure.ModelElement;
import org.modelio.metamodel.uml.infrastructure.Stereotype;
import org.modelio.metamodel.uml.statik.Association;
import org.modelio.metamodel.uml.statik.AssociationEnd;
import org.modelio.metamodel.uml.statik.Attribute;
import org.modelio.metamodel.uml.statik.Classifier;
import org.modelio.metamodel.uml.statik.NameSpace;
import org.modelio.metamodel.uml.statik.Package;
import org.modelio.metamodel.uml.statik.VisibilityMode;
import org.modelio.microservicesnetcore.api.ModuleConstants;
import org.modelio.microservicesnetcore.api.ModuleStereotype;

public class PsmModelBuilder {
	public static void CreatePimDependency(IModelingSession session,ModelElement pimElt,ModelElement psmElt)
	{
		// Stereotype PimPsmDependency
		Stereotype pimImpactStereotype = ModuleStereotype.GetStereotype(session, Package.class, ModuleStereotype.STEREO_PIMDependency);

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
	

	public static ModelElement CreatePsmMicroserviceModel(IModelingSession session, Package visited, ModelElement psmOwner) 
	{
		ModelElement psmElt = CreatePsmGenericPackage(session,visited,psmOwner);
		
		// Stereotype PSM
		Stereotype psmStereo = ModuleStereotype.GetStereotype(session, Package.class, ModuleStereotype.STEREO_PSM_MODEL);
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

	public static Classifier createClassModel(IModelingSession session,Classifier visited, Package psmOwner ) {
		Classifier psmElt;
		IUmlModel model = session.getModel();

		//Create Class
		psmElt = model.createClass(visited.getName(), psmOwner);
		
		// Stereotype PimDependency
		CreatePimDependency(session ,visited,psmElt);
		
		return psmElt;
	}

	public static Attribute createAttribute(Attribute visited, Classifier owner, IModelingSession session) {
		// TODO Auto-generated method stub
		Attribute psmElt;
		IUmlModel model = session.getModel();
		
		//Create Attribute
		psmElt = model.createAttribute(visited.getName(), visited.getType(), owner);

		//Ajout des modifier
		psmElt.setVisibility(VisibilityMode.PUBLIC);
		psmElt.setMultiplicityMin(visited.getMultiplicityMin());
		psmElt.setMultiplicityMax(visited.getMultiplicityMax());

		// Stereotype PimDependency
		CreatePimDependency(session ,visited,psmElt);
		
		return psmElt;
	}

	public static AssociationEnd createAssociationEnd(AssociationEnd visited, IModelingSession _session) {
		// TODO Auto-generated method stub
		AssociationEnd psmElt;
		Classifier pimSrcClass = visited.getSource();
		Classifier pimTargetClass = visited.getTarget();
		Classifier psmSrcClass = (Classifier)PimPsmMapper.GetPsmFromPim(pimSrcClass);
		Classifier psmTargetClass = (Classifier)PimPsmMapper.GetPsmFromPim(pimTargetClass);

		IUmlModel model = _session.getModel();

		Association newAssociation = model.createAssociation(psmSrcClass, psmTargetClass, visited.getName());
		psmElt = newAssociation.getEnd().get(1);
		psmElt.setVisibility(VisibilityMode.PUBLIC);
		psmElt.setMultiplicityMax(visited.getMultiplicityMax());
		psmElt.getOpposite().setMultiplicityMax(visited.getOpposite().getMultiplicityMax());
		psmElt.setAggregation(visited.getAggregation());

		// Stereotype PimDependency
		CreatePimDependency(_session ,visited,psmElt);
		
		return psmElt;
	}
	
}
