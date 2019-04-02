package org.modelio.microservicesnetcore.code.generator.interfaces;

import java.util.List;

import org.modelio.metamodel.uml.statik.Classifier;
import org.modelio.metamodel.uml.statik.Operation;

public interface InterfaceRepoProjectTemplate {

	String getCsProj();

	String getHeader(Classifier visited, Classifier entity);

	String getOperation(Operation visited);

	String getEnd();

	String getUnitOfWork(List<String> iRepositories);

}