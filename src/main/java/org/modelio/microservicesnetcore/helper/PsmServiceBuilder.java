package org.modelio.microservicesnetcore.helper;

import org.modelio.api.modelio.model.IModelingSession;
import org.modelio.api.modelio.model.IUmlModel;
import org.modelio.metamodel.uml.infrastructure.Dependency;
import org.modelio.metamodel.uml.infrastructure.ModelElement;
import org.modelio.metamodel.uml.infrastructure.Stereotype;
import org.modelio.metamodel.uml.statik.Attribute;
import org.modelio.metamodel.uml.statik.Classifier;
import org.modelio.metamodel.uml.statik.GeneralClass;
import org.modelio.metamodel.uml.statik.NameSpace;
import org.modelio.metamodel.uml.statik.Operation;
import org.modelio.metamodel.uml.statik.Package;
import org.modelio.metamodel.uml.statik.Parameter;
import org.modelio.metamodel.uml.statik.VisibilityMode;
import org.modelio.microservicesnetcore.api.ModuleConstants;
import org.modelio.microservicesnetcore.api.ModuleStereotype;

public class PsmServiceBuilder {
	
	public static void CreatePimDependency(IModelingSession session,ModelElement pimElt,ModelElement psmElt)
	{
		// Stereotype PimPsmDependency
		Stereotype pimImpactStereotype = ModuleStereotype.GetStereotype(session, Dependency.class, ModuleStereotype.STEREO_PSMServiceDependency);

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

	public static Classifier createService(IModelingSession session,Classifier visited, Package psmOwner ) {
		Classifier psmElt;
		IUmlModel model = session.getModel();
		
		//Create Class
		psmElt = model.createClass(ModuleConstants.getServiceName(visited.getName()), psmOwner);
		
//		createGetAllOperation(session, psmElt);
//		createGetByIdOperation(session, psmElt);
//		createSaveOrUpdateOperation(session, psmElt);
//		createDeleteOperation(session, psmElt);
		
		// Stereotype PimDependency
		CreatePimDependency(session ,visited,psmElt);
		
		return psmElt;
	}

	public static Operation createGetAllOperation(IModelingSession session,  Classifier psmOwner) {
		Operation newOperation=null;
		
		IUmlModel model = session.getModel();
		
		//Create Operation
		newOperation = model.createOperation("GetAll", psmOwner);
		newOperation.setVisibility(VisibilityMode.PUBLIC);

		//Création du paramètre de retour
		Parameter outParam = model.createParameter();
		outParam.setType((GeneralClass)psmOwner);
		newOperation.setReturn(outParam);
		newOperation.getReturn().setMultiplicityMax("*");
		
		return newOperation;
		
	}
	
	public static Operation createGetByIdOperation(IModelingSession session, Classifier psmOwner) {
		Operation newOperation=null;
		
		IUmlModel model = session.getModel();
		
		//Create Operation
		newOperation = model.createOperation("GetById", psmOwner);
		newOperation.setVisibility(VisibilityMode.PUBLIC);

		//Création du paramètre de retour
		Parameter outParam = model.createParameter();
		outParam.setType((GeneralClass)psmOwner);
		newOperation.setReturn(outParam);
		newOperation.getReturn().setMultiplicityMax("1");
		
		//Création des paramètres d'entrée
		Attribute idAttr=null;
		for(Attribute attr : psmOwner.getOwnedAttribute())
		{
			if(PsmStereotypeValidator.isIdAttribute(attr))
			{
				idAttr=attr;
				break;
			}
		}
		if(idAttr!=null)
		{
			Parameter indexParam = model.createParameter();
			indexParam.setName("id");
			indexParam.setType(idAttr.getType());
			indexParam.setMultiplicityMax("1");
			newOperation.getIO().add(indexParam);
		}

		
		return newOperation;
	}
	
	public static Operation createSaveOrUpdateOperation(IModelingSession session, Classifier psmOwner) {
		Operation newOperation=null;
		
		IUmlModel model = session.getModel();
		
		//Create Operation
		newOperation = model.createOperation("SaveOrUpdate", psmOwner);
		newOperation.setVisibility(VisibilityMode.PUBLIC);

		//Création du paramètre de retour
		Parameter outParam = model.createParameter();
		outParam.setType((GeneralClass)psmOwner);
		newOperation.setReturn(outParam);
		newOperation.getReturn().setMultiplicityMax("1");
		
		//Création des paramètres d'entrée
		Parameter dataParam = model.createParameter();
		dataParam.setName("sObject");
		dataParam.setType((GeneralClass)psmOwner);
		dataParam.setMultiplicityMax("1");
		newOperation.getIO().add(dataParam);

		return newOperation;
	}
	
	public static Operation createDeleteOperation(IModelingSession session, Classifier psmOwner) {
		Operation newOperation=null;
		
		IUmlModel model = session.getModel();
		
		//Create Operation
		newOperation = model.createOperation("Delete", psmOwner);
		newOperation.setVisibility(VisibilityMode.PUBLIC);

		
		//Création des paramètres d'entrée
		Parameter dataParam = model.createParameter();
		dataParam.setName("sObject");
		dataParam.setType((GeneralClass)psmOwner);
		dataParam.setMultiplicityMax("1");
		newOperation.getIO().add(dataParam);

		return newOperation;
	}
	
}
