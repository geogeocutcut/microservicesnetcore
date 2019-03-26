package org.modelio.modeliotools.treevisitor;


import org.modelio.metamodel.uml.infrastructure.ModelTree;
import org.modelio.metamodel.uml.statik.AssociationEnd;
import org.modelio.metamodel.uml.statik.Attribute;
import org.modelio.metamodel.uml.statik.Classifier;
import org.modelio.metamodel.uml.statik.Operation;
import org.modelio.metamodel.uml.statik.Package;

public class OwnedVisitor {
	
	private AbstractHandler handler;
	
	public OwnedVisitor(AbstractHandler sHandler)
	{
		handler = sHandler;
	}
	
	public void process(Package visited){
		handler.preVisitingPackage(visited);
		for (ModelTree element : visited.getOwnedElement()) {
			if (element instanceof Package) process((Package)element);
			if (element instanceof Classifier) process((Classifier)element);
		}
		handler.postVisitingPackage(visited);
	}

	private void process(Classifier visited) {
		handler.preVisitingClassifier(visited);
		for (Operation element : visited.getOwnedOperation()) process(element);
		for (Attribute element : visited.getOwnedAttribute()) process(element);
		for (AssociationEnd element : visited.getOwnedEnd()) 
			process(visited, element);
		handler.postVisitingClassifier(visited);
		
	}

	private void process(Classifier owner, AssociationEnd targetAssociationEnd) {
		handler.preVisitingAssociationEnd( targetAssociationEnd);
		handler.postVisitingAssociationEnd( targetAssociationEnd);
	}

	private void process(Operation visited) {
		handler.preVisitingOperation(visited);
		handler.postVisitingOperation(visited);
	}

	private void process(Attribute visited) {
		handler.preVisitingAttribute(visited);
		handler.postVisitingAttribute(visited);
		
	}

}