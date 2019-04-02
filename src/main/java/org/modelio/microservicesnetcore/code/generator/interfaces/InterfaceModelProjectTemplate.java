package org.modelio.microservicesnetcore.code.generator.interfaces;

import org.modelio.metamodel.uml.statik.AssociationEnd;
import org.modelio.metamodel.uml.statik.Attribute;
import org.modelio.metamodel.uml.statik.Classifier;

public interface InterfaceModelProjectTemplate {

	String getCsProj();

	String getHeader(Classifier visited);

	String getEnd();

	String getAttribute(Attribute attr);

	String getOnetomany(AssociationEnd end);

	String getOnetoone(AssociationEnd end);

}