package org.modelio.microservicesnetcore.code.generator.template;

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
import org.modelio.metamodel.uml.statik.Classifier;
import org.modelio.metamodel.uml.statik.GeneralClass;
import org.modelio.metamodel.uml.statik.Package;
import org.modelio.microservicesnetcore.impl.MicroserviceDotnetCoreModule;

public class HbmTemplate {
	private String header= "org/modelio/microservicesnetcore/template/06 - hbm/header.txt";
	private String end = "org/modelio/microservicesnetcore/template/06 - hbm/end.txt";
	private String attribute = "org/modelio/microservicesnetcore/template/06 - hbm/attribute.txt";
	private String onetoone = "org/modelio/microservicesnetcore/template/06 - hbm/onetoone.txt";
	private String onetomany = "org/modelio/microservicesnetcore/template/06 - hbm/onetomany.txt";
	private String manytoone = "org/modelio/microservicesnetcore/template/06 - hbm/manytoone.txt";
	private String manytomany = "org/modelio/microservicesnetcore/template/06 - hbm/manytomany.txt";
	private String _application;
	private Package _domain;
	private IUMLTypes umlType;
	
	
	public HbmTemplate(String application,Package domain) {
		_application = application;
		_domain = domain;
		IModule module = MicroserviceDotnetCoreModule.getInstance();
		IModelingSession session = module.getModuleContext().getModelingSession();
		IUmlModel model = session.getModel();
		umlType = model.getUmlTypes();
	}
	
	public String getHeader(Classifier entity) {
		StringBuilder tmpl = new StringBuilder();
		try {
			InputStream stream = getClass().getClassLoader().getResourceAsStream(header);
			BufferedReader  reader = new BufferedReader (new InputStreamReader(stream, Charset.forName("UTF-8")));
			while (reader.ready()) {
				tmpl.append(reader.readLine()).append("\n");
			}
		} catch (IOException e) {
			e.printStackTrace();
		}
		
		String result = tmpl.toString();
		result = result.replaceAll("@@entity", entity.getName());
		result = result.replaceAll("@@application", _application);
		result = result.replaceAll("@@domain", _domain.getName());
		
		return result;
	}
	public String getEnd() {
		StringBuilder tmpl = new StringBuilder();
		try {
			InputStream stream = getClass().getClassLoader().getResourceAsStream(end);
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
			InputStream stream = getClass().getClassLoader().getResourceAsStream(attribute);
			BufferedReader  reader = new BufferedReader (new InputStreamReader(stream, Charset.forName("UTF-8")));
			while (reader.ready()) {
				tmpl.append(reader.readLine()).append("\n");
			}
		} catch (IOException e) {
			e.printStackTrace();
		}
		String result = tmpl.toString();
		result = result.replaceAll("@@name", attr.getName());
		result = result.replaceAll("@@column", attr.getName());
		result = result.replaceAll("@@type", getHbmTypeFromUmlType(attr.getType()));
		return result;
	}
	
	public String getOnetoone(AssociationEnd end) {
		StringBuilder tmpl = new StringBuilder();
		try {
			InputStream stream = getClass().getClassLoader().getResourceAsStream(onetoone);
			BufferedReader  reader = new BufferedReader (new InputStreamReader(stream, Charset.forName("UTF-8")));
			while (reader.ready()) {
				tmpl.append(reader.readLine()).append("\n");
			}
		} catch (IOException e) {
			e.printStackTrace();
		}
		String result = formatEndCode(end, tmpl);
		return result;
	}
	public String getOnetomany(AssociationEnd end) {
		StringBuilder tmpl = new StringBuilder();
		try {
			InputStream stream = getClass().getClassLoader().getResourceAsStream(onetomany);
			BufferedReader  reader = new BufferedReader (new InputStreamReader(stream, Charset.forName("UTF-8")));
			while (reader.ready()) {
				tmpl.append(reader.readLine()).append("\n");
			}
		} catch (IOException e) {
			e.printStackTrace();
		}
		String result = formatEndCode(end, tmpl);
		return result;
	}

	
	public String getManytoone(AssociationEnd end) {
		StringBuilder tmpl = new StringBuilder();
		try {
			InputStream stream = getClass().getClassLoader().getResourceAsStream(manytoone);
			BufferedReader  reader = new BufferedReader (new InputStreamReader(stream, Charset.forName("UTF-8")));
			while (reader.ready()) {
				tmpl.append(reader.readLine()).append("\n");
			}
		} catch (IOException e) {
			e.printStackTrace();
		}
		String result = formatEndCode(end, tmpl);
		return result;
	}
	
	public String getManytomany(AssociationEnd end) {
		StringBuilder tmpl = new StringBuilder();
		try {
			InputStream stream = getClass().getClassLoader().getResourceAsStream(manytomany);
			BufferedReader  reader = new BufferedReader (new InputStreamReader(stream, Charset.forName("UTF-8")));
			while (reader.ready()) {
				tmpl.append(reader.readLine()).append("\n");
			}
		} catch (IOException e) {
			e.printStackTrace();
		}
		String result = formatEndCode(end, tmpl);
		return result;
	}
	
	private String formatEndCode(AssociationEnd end, StringBuilder tmpl) {
		String result = tmpl.toString();
		Classifier entity = (Classifier)end.getSource();
		result = result.replaceAll("@@name", end.getName());
		result = result.replaceAll("@@column", end.getName().toUpperCase());
		result = result.replaceAll("@@application", _application);
		result = result.replaceAll("@@domain", _domain.getName());
		result = result.replaceAll("@@entity", entity.getName());
		result = result.replaceAll("@@targetentity", end.getTarget().getName());
		return result;
	}
	
	private String getHbmTypeFromUmlType(GeneralClass type) {
		String result = null;
		if (type.getUuid().equals(umlType.getSTRING().getUuid()))
			result = "string";
		else if (type.getUuid().equals(umlType.getDOUBLE().getUuid()))
			result = "double";
		else if (type.getUuid().equals(umlType.getINTEGER().getUuid()))
			result = "int";
		else if (type.getUuid().equals(umlType.getBOOLEAN().getUuid()))
			result = "boolean";
		else if (type.getUuid().equals(umlType.getDATE().getUuid()))
			result = "date";
		else
			result = "string";
		
		return result;
	}

}
