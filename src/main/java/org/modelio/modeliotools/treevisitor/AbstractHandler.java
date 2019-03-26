package org.modelio.modeliotools.treevisitor;

import java.util.Stack;

import org.modelio.metamodel.uml.statik.AssociationEnd;
import org.modelio.metamodel.uml.statik.Attribute;
import org.modelio.metamodel.uml.statik.Classifier;
import org.modelio.metamodel.uml.statik.Operation;

public abstract class AbstractHandler {
	Stack<Object> context = new Stack<Object>();
	
	public final void preVisitingClassifier(Classifier visited){
		context.push(visited);
		beginVisitingClassifier(visited);
	}
	protected abstract void beginVisitingClassifier(Classifier visited);
	protected abstract void endVisitingClassifier(Classifier visited);
	public final void postVisitingClassifier(Classifier visited){
		endVisitingClassifier(visited);
		context.pop();
	}
	
	public final void preVisitingPackage(org.modelio.metamodel.uml.statik.Package visited){
		context.push(visited);
		beginVisitingPackage(visited);
	}
	protected abstract void beginVisitingPackage(org.modelio.metamodel.uml.statik.Package visited);
	protected abstract void endVisitingPackage(org.modelio.metamodel.uml.statik.Package visited);
	public final void postVisitingPackage(org.modelio.metamodel.uml.statik.Package visited){
		endVisitingPackage(visited);
		context.pop();
	}
	
	public final void preVisitingAttribute(Attribute visited){
		context.push(visited);
		beginVisitingAttribute(visited);;
	}
	protected abstract void beginVisitingAttribute(Attribute visited);
	protected abstract void endVisitingAttribute(Attribute visited);
	public final void postVisitingAttribute(Attribute visited){
		endVisitingAttribute(visited);
		context.pop();
	}
	
	public final void preVisitingOperation(Operation visited){
		context.push(visited);
		beginVisitingOperation(visited);
	}
	protected abstract void beginVisitingOperation(Operation visited);
	protected abstract void endVisitingOperation(Operation visited);
	public final void postVisitingOperation(Operation visited){
		endVisitingOperation(visited);
		context.pop();
	}
	public final void preVisitingAssociationEnd(AssociationEnd targetAssociationEnd) {
		context.push(targetAssociationEnd);
		beginVisitingAssociationEnd(targetAssociationEnd);
	}
	protected abstract void beginVisitingAssociationEnd(AssociationEnd targetAssociationEnd);
	protected abstract void endVisitingAssociationEnd(AssociationEnd targetAssociationEnd);
	public final void postVisitingAssociationEnd(AssociationEnd targetAssociationEnd) {
		endVisitingAssociationEnd(targetAssociationEnd);
		context.pop();
	}
	
}
