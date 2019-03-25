package org.modelio.microservicesnetcore.code.generator;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.nio.charset.Charset;


import org.modelio.api.modelio.model.IModelingSession;
import org.modelio.api.modelio.model.IUMLTypes;
import org.modelio.api.modelio.model.IUmlModel;
import org.modelio.api.module.IModule;
import org.modelio.metamodel.uml.statik.AssociationEnd;
import org.modelio.metamodel.uml.statik.Attribute;
import org.modelio.metamodel.uml.statik.Class;
import org.modelio.metamodel.uml.statik.Classifier;
import org.modelio.metamodel.uml.statik.GeneralClass;
import org.modelio.metamodel.uml.statik.Package;
import org.modelio.microservicesnetcore.impl.MicroserviceDotnetCoreModule;

public class ModelProjectTemplate {
	private String _header= "org/modelio/microservicesnetcore/template/01 - model/header.txt";
	private String _end = "org/modelio/microservicesnetcore/template/00 - common/end.txt";
	private String _attribute = "org/modelio/microservicesnetcore/template/00 - common/attribute.txt";
	private String _onetomany = "org/modelio/microservicesnetcore/template/00 - common/onetomany.txt";
	private String _csproj = "org/modelio/microservicesnetcore/template/01 - model/csproj.txt";
	private IUMLTypes _umlType;
	
	private String _applicationName="";
	private Package _domain;
	
	public ModelProjectTemplate(String applicationName, Package domain) {
		IModule module = MicroserviceDotnetCoreModule.getInstance();
		IModelingSession session = module.getModuleContext().getModelingSession();
		IUmlModel model = session.getModel();
		_umlType = model.getUmlTypes();
		_domain=domain;
		_applicationName=applicationName;
	}

	public String getCsProj() {
		StringBuilder tmpl = new StringBuilder();
		try {
			InputStream stream = ModelProjectTemplate.class.getClassLoader().getResourceAsStream(_csproj);
			BufferedReader  reader = new BufferedReader (new InputStreamReader(stream, Charset.forName("UTF-8")));
			while (reader.ready()) {
				tmpl.append(reader.readLine()).append("\n");
			}
		} catch (IOException e) {
			e.printStackTrace();
		}
		
		
		return tmpl.toString();
	}
	
	public String getHeader(Classifier visited) {
		StringBuilder tmpl = new StringBuilder();
		try {
			InputStream stream = ModelProjectTemplate.class.getClassLoader().getResourceAsStream(_header);
			BufferedReader  reader = new BufferedReader (new InputStreamReader(stream, Charset.forName("UTF-8")));
			while (reader.ready()) {
				tmpl.append(reader.readLine()).append("\n");
			}
		} catch (IOException e) {
			e.printStackTrace();
		}
		
		String result = tmpl.toString();
		result = result.replaceAll("@@entity", visited.getName());
		result = result.replaceAll("@@domain", _domain.getName());
		result = result.replaceAll("@@application", _applicationName);
		
		return result;
	}
	
	public String getEnd() {
		StringBuilder tmpl = new StringBuilder();
		try {
			InputStream stream = ModelProjectTemplate.class.getClassLoader().getResourceAsStream(_end);
			BufferedReader  reader = new BufferedReader (new InputStreamReader(stream, Charset.forName("UTF-8")));
			while (reader.ready()) {
				tmpl.append(reader.readLine()).append("\n");
			}
		} catch (IOException e) {
			e.printStackTrace();
		}
		
		
		return tmpl.toString();
	}
	public String getAttribute(Attribute attr) {
		StringBuilder tmpl = new StringBuilder();
		try {
			InputStream stream = ModelProjectTemplate.class.getClassLoader().getResourceAsStream(_attribute);
			BufferedReader  reader = new BufferedReader (new InputStreamReader(stream, Charset.forName("UTF-8")));
			while (reader.ready()) {
				tmpl.append(reader.readLine()).append("\n");
			}
		} catch (IOException e) {
			e.printStackTrace();
		}
		String result = tmpl.toString();
		result = result.replaceAll("@@visibility", "public");
		result = result.replaceAll("@@name", attr.getName());
		result = result.replaceAll("@@type", getNetTypeFromUmlType(attr.getType()));
		return result;
	}
	
	public String getOnetomany(AssociationEnd end) {
		StringBuilder tmpl = new StringBuilder();
		try {
			InputStream stream = ModelProjectTemplate.class.getClassLoader().getResourceAsStream(_onetomany);
			BufferedReader  reader = new BufferedReader (new InputStreamReader(stream, Charset.forName("UTF-8")));
			while (reader.ready()) {
				tmpl.append(reader.readLine()).append("\n");
			}
		} catch (IOException e) {
			e.printStackTrace();
		}
		
		String result = tmpl.toString();
		Class entity = (Class)end.getSource();
		result = result.replaceAll("@@name", end.getName());
		result = result.replaceAll("@@type", entity.getName());
		
		return result;
	}

	
	private String getNetTypeFromUmlType(GeneralClass type) {
		String result = null;
		if (type.getUuid().equals(_umlType.getSTRING().getUuid()))
			result = "string";
		else if (type.getUuid().equals(_umlType.getDOUBLE().getUuid()))
			result = "double";
		else if (type.getUuid().equals(_umlType.getINTEGER().getUuid()))
			result = "int";
		else if (type.getUuid().equals(_umlType.getBOOLEAN().getUuid()))
			result = "bool";
		else if (type.getUuid().equals(_umlType.getDATE().getUuid()))
			result = "DateTime";
		else
			result = "string";
		
		return result;
	}
}