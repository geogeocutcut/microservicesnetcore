package org.modelio.microservicesnetcore.code.generator.template;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.nio.charset.Charset;

import org.modelio.metamodel.uml.statik.Classifier;
import org.modelio.metamodel.uml.statik.Operation;
import org.modelio.metamodel.uml.statik.Package;
import org.modelio.metamodel.uml.statik.Parameter;
import org.modelio.microservicesnetcore.helper.ModuleHelper;

public class IRepositoryProjectTemplate {
	private String _header= "org/modelio/microservicesnetcore/template/02 - repository/interface/header.txt";
	private String _end = "org/modelio/microservicesnetcore/template/00 - common/end.txt";
	
	private String _asyncopeheadervoid = "org/modelio/microservicesnetcore/template/00 - common/asyncioperationheadervoid.txt";
	private String _asyncopeheaderwithreturn = "org/modelio/microservicesnetcore/template/00 - common/asyncioperationheaderwithreturn.txt";
	private String _opeparameter = "org/modelio/microservicesnetcore/template/00 - common/operationparameter.txt";
	private String _asyncopeend = "org/modelio/microservicesnetcore/template/00 - common/asyncioperationend.txt";
	
	private String _csproj = "org/modelio/microservicesnetcore/template/02 - repository/interface/csproj.txt";
	private String _iunitofwork = "org/modelio/microservicesnetcore/template/02 - repository/interface/iunitofwork.txt";
	
	private String _applicationName;
	private Package _domain;
	
	public IRepositoryProjectTemplate(String applicationName, Package domain) {
		_applicationName=applicationName;
		_domain=domain;
	}

	public String getCsProj() {
		StringBuilder tmpl = new StringBuilder();
		try {
			InputStream stream = IRepositoryProjectTemplate.class.getClassLoader().getResourceAsStream(_csproj);
			BufferedReader  reader = new BufferedReader (new InputStreamReader(stream, Charset.forName("UTF-8")));
			while (reader.ready()) {
				tmpl.append(reader.readLine()).append("\n");
			}
		} catch (IOException e) {
			e.printStackTrace();
		}
		
		
		String result = tmpl.toString();
		result = result.replaceAll("@@domain", _domain.getName());
		result = result.replaceAll("@@application", _applicationName);
		
		return result;
	}
	
	public String getHeader(Classifier visited,Classifier entity) {
		StringBuilder tmpl = new StringBuilder();
		try {
			InputStream stream = IRepositoryProjectTemplate.class.getClassLoader().getResourceAsStream(_header);
			BufferedReader  reader = new BufferedReader (new InputStreamReader(stream, Charset.forName("UTF-8")));
			while (reader.ready()) {
				tmpl.append(reader.readLine()).append("\n");
			}
		} catch (IOException e) {
			e.printStackTrace();
		}
		
		
		String result = tmpl.toString();
		result = result.replaceAll("@@name", "I"+visited.getName());
		result = result.replaceAll("@@entity", entity.getName());
		result = result.replaceAll("@@domain", _domain.getName());
		result = result.replaceAll("@@application", _applicationName);
		
		return result;
	}
	
	public String getOperation(Operation visited) {
		// header
		StringBuilder tmpl = new StringBuilder();
		
		String opeHeader=visited.getReturn()!=null?_asyncopeheaderwithreturn:_asyncopeheadervoid;
		String returnType=visited.getReturn()!=null?ModuleHelper.getNetTypeFromUmlType(visited.getReturn().getType()):"";
		if(!returnType.isEmpty() && visited.getReturn().getMultiplicityMax()=="*")
		{
			returnType="IList<"+returnType+">";
		}
		try {
			InputStream stream = IRepositoryProjectTemplate.class.getClassLoader().getResourceAsStream(opeHeader);
			BufferedReader  reader = new BufferedReader (new InputStreamReader(stream, Charset.forName("UTF-8")));
			while (reader.ready()) {
				tmpl.append(reader.readLine());
			}
		} catch (IOException e) {
			e.printStackTrace();
		}
		
		String result = tmpl.toString();
		result = result.replaceAll("@@name", visited.getName());
		result = result.replaceAll("@@returnType", returnType);
		
		// parameter
		tmpl = new StringBuilder();
		try {
			InputStream stream = IRepositoryProjectTemplate.class.getClassLoader().getResourceAsStream(_opeparameter);
			BufferedReader  reader = new BufferedReader (new InputStreamReader(stream, Charset.forName("UTF-8")));
			while (reader.ready()) {
				tmpl.append(reader.readLine());
			}
		} catch (IOException e) {
			e.printStackTrace();
		}
		String paramListStr = "";
		for(Parameter p : visited.getIO() )
		{
			if(!paramListStr.isEmpty())paramListStr+=",";
			String paramStr= tmpl.toString();
			paramStr=paramStr.replaceAll("@@name", p.getName());
			paramStr=paramStr.replaceAll("@@type", ModuleHelper.getNetTypeFromUmlType(p.getType()));
			paramListStr+=paramStr;
		}

		result+=paramListStr;
		
		
		// end
		tmpl = new StringBuilder();
		try {
			InputStream stream = IRepositoryProjectTemplate.class.getClassLoader().getResourceAsStream(_asyncopeend);
			BufferedReader  reader = new BufferedReader (new InputStreamReader(stream, Charset.forName("UTF-8")));
			while (reader.ready()) {
				tmpl.append(reader.readLine()).append("\n");
			}
		} catch (IOException e) {
			e.printStackTrace();
		}
		result +=tmpl.toString();
		
		return result;
	}
	
	public String getEnd() {
		StringBuilder tmpl = new StringBuilder();
		try {
			InputStream stream = IRepositoryProjectTemplate.class.getClassLoader().getResourceAsStream(_end);
			BufferedReader  reader = new BufferedReader (new InputStreamReader(stream, Charset.forName("UTF-8")));
			while (reader.ready()) {
				tmpl.append(reader.readLine()).append("\n");
			}
		} catch (IOException e) {
			e.printStackTrace();
		}
		
		
		return tmpl.toString();
	}
	
	public String getIUnitOfWork()
	{
		StringBuilder tmpl = new StringBuilder();
		try {
			InputStream stream = IRepositoryProjectTemplate.class.getClassLoader().getResourceAsStream(_iunitofwork);
			BufferedReader  reader = new BufferedReader (new InputStreamReader(stream, Charset.forName("UTF-8")));
			while (reader.ready()) {
				tmpl.append(reader.readLine()).append("\n");
			}
		} catch (IOException e) {
			e.printStackTrace();
		}
		
		
		String result = tmpl.toString();
		result = result.replaceAll("@@domain", _domain.getName());
		result = result.replaceAll("@@application", _applicationName);
		
		return result;
	}
}
