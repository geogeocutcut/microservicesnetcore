package org.modelio.microservicesnetcore.helper;

import org.modelio.api.modelio.model.IModelingSession;
import org.modelio.api.modelio.model.IUmlModel;
import org.modelio.metamodel.uml.infrastructure.Dependency;
import org.modelio.metamodel.uml.infrastructure.ModelElement;
import org.modelio.metamodel.uml.infrastructure.ModelTree;
import org.modelio.metamodel.uml.infrastructure.Stereotype;
import org.modelio.metamodel.uml.statik.Association;
import org.modelio.metamodel.uml.statik.AssociationEnd;
import org.modelio.metamodel.uml.statik.Attribute;
import org.modelio.metamodel.uml.statik.Classifier;
import org.modelio.metamodel.uml.statik.Enumeration;
import org.modelio.metamodel.uml.statik.EnumerationLiteral;
import org.modelio.metamodel.uml.statik.NameSpace;
import org.modelio.metamodel.uml.statik.Package;
import org.modelio.metamodel.uml.statik.VisibilityMode;
import org.modelio.microservicesnetcore.api.ModuleStereotype;

public class PsmModelBuilder {
	
	public static void CreatePimDependency(IModelingSession session,ModelElement pimElt,ModelElement psmElt)
	{
		// Stereotype PimPsmDependency
		Stereotype pimImpactStereotype = ModuleStereotype.GetStereotype(session, Dependency.class, ModuleStereotype.STEREO_PSMModelDependency);

		IUmlModel model= session.getModel();
		model.createDependency(psmElt, pimElt,pimImpactStereotype);
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
		psmElt = model.createClass(StringConverter.SnakeCaseToCamelCase(visited.getName()), psmOwner);
		
		// Stereotype PimDependency
		CreatePimDependency(session ,visited,psmElt);
		
		return psmElt;
	}

	public static Attribute createAttribute(IModelingSession session,Attribute visited, Classifier owner) {
		// TODO Auto-generated method stub
		Attribute psmElt=null;
		IUmlModel model = session.getModel();
		
		//Create Attribute
		try {
			psmElt = model.createAttribute(StringConverter.SnakeCaseToCamelCase(visited.getName()), visited.getType(), owner);
		}
		catch(Exception ex)
		{
			int i = 0;
		}

		//Ajout des modifier
		psmElt.setVisibility(VisibilityMode.PUBLIC);
		psmElt.setMultiplicityMin(visited.getMultiplicityMin());
		psmElt.setMultiplicityMax(visited.getMultiplicityMax());

		// Stereotype PimDependency
		CreatePimDependency(session ,visited,psmElt);
		
		return psmElt;
	}

	public static AssociationEnd createAssociationEnd(IModelingSession _session,AssociationEnd visited) {
		// TODO Auto-generated method stub
		AssociationEnd psmElt;
		Classifier pimSrcClass = visited.getSource();
		Classifier pimTargetClass = visited.getTarget();
		Classifier psmSrcClass = (Classifier)PimPsmMapper.GetPsmModelFromPim(pimSrcClass);
		Classifier psmTargetClass = (Classifier)PimPsmMapper.GetPsmModelFromPim(pimTargetClass);

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



	public static Classifier createEnumModel(IModelingSession session, Enumeration visited, Package psmOwner) {
		// TODO Auto-generated method stub
		Enumeration psmElt;
		IUmlModel model = session.getModel();

		//Create Class
		psmElt = model.createEnumeration(StringConverter.SnakeCaseToCamelCase(visited.getName()), psmOwner);
		for(EnumerationLiteral enumValVisited : visited.getValue()) {
			if(enumValVisited instanceof EnumerationLiteral)
			{
				EnumerationLiteral enumValPsm = model.createEnumerationLiteral(((EnumerationLiteral)enumValVisited).getName(), psmElt);
				// Stereotype PimDependency
				CreatePimDependency(session ,enumValVisited,enumValPsm);
			}
		}
		// Stereotype PimDependency
		CreatePimDependency(session ,visited,psmElt);
		
		return psmElt;
	}

}
