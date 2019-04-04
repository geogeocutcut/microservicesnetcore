package org.modelio.microservicesnetcore.helper;

import org.modelio.api.modelio.model.IModelingSession;
import org.modelio.api.modelio.model.IUmlModel;
import org.modelio.metamodel.mda.Project;
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

public class PsmRepositoryBuilder {
	
	public static void CreatePimDependency(IModelingSession session,ModelElement pimElt,ModelElement psmElt)
	{
		// Stereotype PimPsmDependency
		Stereotype pimImpactStereotype = ModuleStereotype.GetStereotype(session, Dependency.class, ModuleStereotype.STEREO_PSMRepositoryDependency);

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
	

	public static ModelElement CreatePsmRepository(IModelingSession session, Package visited, ModelElement psmOwner) 
	{
		ModelElement psmElt = CreatePsmGenericPackage(session,visited,psmOwner);
		
		// Stereotype PSM
		Stereotype psmStereo = ModuleStereotype.GetStereotype(session, Package.class, ModuleStereotype.STEREO_PSM_REPOSITORY);
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

	public static Classifier createRepository(IModelingSession session,Classifier visited, Package psmOwner ) {
		Classifier psmElt;
		IUmlModel model = session.getModel();
		
		//Create Class
		psmElt = model.createClass(ModuleConstants.getRepositoryName(visited.getName()), psmOwner);
		
		//Classifier entity = (Classifier)PimPsmMapper.GetPsmModelFromPim(visited);
		//createGetAllOperation(session, psmElt,entity);
		//createGetByIdOperation(session, psmElt,entity);
		//createSaveOrUpdateOperation(session, psmElt,entity);
		//createDeleteOperation(session, psmElt,entity);
		
		
		// Stereotype PimDependency
		CreatePimDependency(session ,visited,psmElt);
		
		return psmElt;
	}

	public static Operation createGetAllOperation(IModelingSession session,  Classifier psmOwner,  Classifier entity) {
		Operation newOperation=null;
		
		IUmlModel model = session.getModel();
		
		//Create Operation
		newOperation = model.createOperation("GetAll", psmOwner);
		newOperation.setVisibility(VisibilityMode.PUBLIC);

		//Création du paramètre de retour
		Parameter outParam = model.createParameter();
		outParam.setType((GeneralClass)entity);
		newOperation.setReturn(outParam);
		newOperation.getReturn().setMultiplicityMax("*");
		
		return newOperation;
		
	}
	
	public static Operation createGetByIdOperation(IModelingSession session, Classifier psmOwner,  Classifier entity) {
		Operation newOperation=null;
		
		IUmlModel model = session.getModel();
		
		//Create Operation
		newOperation = model.createOperation("GetById", psmOwner);
		newOperation.setVisibility(VisibilityMode.PUBLIC);

		//Création du paramètre de retour
		Parameter outParam = model.createParameter();
		outParam.setType((GeneralClass)entity);
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
	
	public static Operation createSaveOrUpdateOperation(IModelingSession session, Classifier psmOwner,  Classifier entity) {
		Operation newOperation=null;
		
		IUmlModel model = session.getModel();
		
		//Create Operation
		newOperation = model.createOperation("Upsert", psmOwner);
		newOperation.setVisibility(VisibilityMode.PUBLIC);

		//Création du paramètre de retour
		Parameter outParam = model.createParameter();
		outParam.setType((GeneralClass)entity);
		newOperation.setReturn(outParam);
		newOperation.getReturn().setMultiplicityMax("1");
		
		//Création des paramètres d'entrée
		Parameter dataParam = model.createParameter();
		dataParam.setName("sObject");
		dataParam.setType((GeneralClass)entity);
		dataParam.setMultiplicityMax("1");
		newOperation.getIO().add(dataParam);

		return newOperation;
	}
	
	public static Operation createDeleteOperation(IModelingSession session, Classifier psmOwner,  Classifier entity) {
		Operation newOperation=null;
		
		IUmlModel model = session.getModel();
		
		//Create Operation
		newOperation = model.createOperation("Delete", psmOwner);
		newOperation.setVisibility(VisibilityMode.PUBLIC);

		
		//Création des paramètres d'entrée
		Parameter dataParam = model.createParameter();
		dataParam.setName("sObject");
		dataParam.setType((GeneralClass)entity);
		dataParam.setMultiplicityMax("1");
		newOperation.getIO().add(dataParam);

		return newOperation;
	}
	
}
