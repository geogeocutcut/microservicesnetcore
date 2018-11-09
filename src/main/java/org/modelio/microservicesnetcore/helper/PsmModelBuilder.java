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
import org.modelio.vcore.smkernel.mapi.MObject;

public class PsmModelBuilder {
	
	
	
	
	public static Classifier createClassModel(IModelingSession session,Classifier visited, Package psmOwner ) {
		Classifier psmElt;
		IUmlModel model = session.getModel();

		//Create Class
		psmElt = model.createClass(visited.getName(), psmOwner);
		
		// Stereotype PimDependency
		PsmBuilder.CreatePimDependency(session ,visited,psmElt);
		
		return psmElt;
	}

	public static Attribute createAttribute(IModelingSession session,Attribute visited, Classifier owner) {
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
		PsmBuilder.CreatePimDependency(session ,visited,psmElt);
		
		return psmElt;
	}

	public static AssociationEnd createAssociationEnd(IModelingSession _session,AssociationEnd visited) {
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
		PsmBuilder.CreatePimDependency(_session ,visited,psmElt);
		
		return psmElt;
	}

}
