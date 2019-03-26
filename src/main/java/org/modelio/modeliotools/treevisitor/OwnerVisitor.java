package org.modelio.modeliotools.treevisitor;


import org.modelio.metamodel.uml.infrastructure.ModelTree;
import org.modelio.metamodel.uml.statik.Classifier;
import org.modelio.metamodel.uml.statik.Package;

public class OwnerVisitor {
	
	private AbstractHandler handler;
	
	public OwnerVisitor(AbstractHandler sHandler)
	{
		handler = sHandler;
	}
	
	public void process(Package visited){
		handler.preVisitingPackage(visited);
		ModelTree element = visited.getOwner();
		if (element instanceof Package) process((Package)element);
		handler.postVisitingPackage(visited);
	}

	public void process(Classifier visited) {
		handler.preVisitingClassifier(visited);
		ModelTree element = visited.getOwner();
		if (element instanceof Package) process((Package)element);
		handler.postVisitingClassifier(visited);
	}
}