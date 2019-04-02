package org.modelio.microservicesnetcore.code.generator.template;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.nio.charset.Charset;
import java.util.List;

import org.modelio.metamodel.uml.statik.Classifier;
import org.modelio.metamodel.uml.statik.Operation;
import org.modelio.metamodel.uml.statik.Package;
import org.modelio.metamodel.uml.statik.Parameter;
import org.modelio.microservicesnetcore.code.generator.interfaces.InterfaceRepoProjectTemplate;
import org.modelio.microservicesnetcore.helper.ModuleHelper;

public class RepositoryInMemoryProjectTemplate implements InterfaceRepoProjectTemplate {
	private String _header= "org/modelio/microservicesnetcore/template/02 - repository/impl/inmemory/header.txt";
	private String _end = "org/modelio/microservicesnetcore/template/00 - common/end.txt";
	
	private String _asyncopeheadervoid = "org/modelio/microservicesnetcore/template/00 - common/asyncoperationheadervoid.txt";
	private String _asyncopeheaderwithreturn = "org/modelio/microservicesnetcore/template/00 - common/asyncoperationheaderwithreturn.txt";
	private String _opeparameter = "org/modelio/microservicesnetcore/template/00 - common/operationparameter.txt";

	private String _asyncopestart = "org/modelio/microservicesnetcore/template/00 - common/asyncoperationstart.txt";
	private String _asyncopeend = "org/modelio/microservicesnetcore/template/00 - common/asyncoperationend.txt";
	
	private String _csproj = "org/modelio/microservicesnetcore/template/02 - repository/impl/inmemory/csproj.txt";
	private String _unitofwork = "org/modelio/microservicesnetcore/template/02 - repository/impl/inmemory/unitofwork.txt";
	private String _iocregister = "org/modelio/microservicesnetcore/template/02 - repository/impl/inmemory/iocregister.txt";
	
	private String _applicationName;
	private Package _domain;
	
	public RepositoryInMemoryProjectTemplate(String applicationName, Package domain) {
		_domain=domain;
		_applicationName=applicationName;
	}

	/* (non-Javadoc)
	 * @see org.modelio.microservicesnetcore.code.generator.InterfaceRepoProjectTemplate#getCsProj()
	 */
	@Override
	public String getCsProj() {
		StringBuilder tmpl = new StringBuilder();
		try {
			InputStream stream = getClass().getClassLoader().getResourceAsStream(_csproj);
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
	
	/* (non-Javadoc)
	 * @see org.modelio.microservicesnetcore.code.generator.InterfaceRepoProjectTemplate#getHeader(org.modelio.metamodel.uml.statik.Classifier, org.modelio.metamodel.uml.statik.Classifier)
	 */
	@Override
	public String getHeader(Classifier visited,Classifier entity) {
		StringBuilder tmpl = new StringBuilder();
		try {
			InputStream stream = getClass().getClassLoader().getResourceAsStream(_header);
			BufferedReader  reader = new BufferedReader (new InputStreamReader(stream, Charset.forName("UTF-8")));
			while (reader.ready()) {
				tmpl.append(reader.readLine()).append("\n");
			}
		} catch (IOException e) {
			e.printStackTrace();
		}
		
		String result = tmpl.toString();
		result = result.replaceAll("@@name", visited.getName());
		result = result.replaceAll("@@entity", entity.getName());
		result = result.replaceAll("@@domain", _domain.getName());
		result = result.replaceAll("@@application", _applicationName);
		
		return result;
	}
	
	/* (non-Javadoc)
	 * @see org.modelio.microservicesnetcore.code.generator.InterfaceRepoProjectTemplate#getOperation(org.modelio.metamodel.uml.statik.Operation)
	 */
	@Override
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
			InputStream stream = getClass().getClassLoader().getResourceAsStream(opeHeader);
			BufferedReader  reader = new BufferedReader (new InputStreamReader(stream, Charset.forName("UTF-8")));
			while (reader.ready()) {
				tmpl.append(reader.readLine()).append("\n");
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
			InputStream stream = getClass().getClassLoader().getResourceAsStream(_opeparameter);
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
		// start
		tmpl = new StringBuilder();
		try {
			InputStream stream = getClass().getClassLoader().getResourceAsStream(_asyncopestart);
			BufferedReader  reader = new BufferedReader (new InputStreamReader(stream, Charset.forName("UTF-8")));
			while (reader.ready()) {
				tmpl.append(reader.readLine()).append("\n");
			}
		} catch (IOException e) {
			e.printStackTrace();
		}
		result +=tmpl.toString();
				
		
		// end
		tmpl = new StringBuilder();
		try {
			InputStream stream = getClass().getClassLoader().getResourceAsStream(_asyncopeend);
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
	
	/* (non-Javadoc)
	 * @see org.modelio.microservicesnetcore.code.generator.InterfaceRepoProjectTemplate#getEnd()
	 */
	@Override
	public String getEnd() {
		StringBuilder tmpl = new StringBuilder();
		try {
			InputStream stream = getClass().getClassLoader().getResourceAsStream(_end);
			BufferedReader  reader = new BufferedReader (new InputStreamReader(stream, Charset.forName("UTF-8")));
			while (reader.ready()) {
				tmpl.append(reader.readLine()).append("\n");
			}
		} catch (IOException e) {
			e.printStackTrace();
		}
		
		
		return tmpl.toString();
	}
	
	/* (non-Javadoc)
	 * @see org.modelio.microservicesnetcore.code.generator.InterfaceRepoProjectTemplate#getUnitOfWork(java.util.List)
	 */
	@Override
	public String getUnitOfWork(List<String> iRepositories)
	{
		
		StringBuilder tmpl = new StringBuilder();
		try {
			InputStream stream = IRepositoryProjectTemplate.class.getClassLoader().getResourceAsStream(_iocregister);
			BufferedReader  reader = new BufferedReader (new InputStreamReader(stream, Charset.forName("UTF-8")));
			while (reader.ready()) {
				tmpl.append(reader.readLine()).append("\n");
			}
		} catch (IOException e) {
			e.printStackTrace();
		}
		String iocRegister="";
		for(String repo : iRepositories)
		{
			iocRegister+=tmpl.toString()
					.replaceAll("@@IRepository", "I"+repo)
					.replaceAll("@@Repository", repo);
		}
		
		tmpl = new StringBuilder();
		try {
			InputStream stream = getClass().getClassLoader().getResourceAsStream(_unitofwork);
			BufferedReader  reader = new BufferedReader (new InputStreamReader(stream, Charset.forName("UTF-8")));
			while (reader.ready()) {
				tmpl.append(reader.readLine()).append("\n");
			}
		} catch (IOException e) {
			e.printStackTrace();
		}
		
		
		String result = tmpl.toString();
		result = result.replaceAll("@@domain", _domain.getName())
				.replaceAll("@@application", _applicationName)
				.replaceAll("@@IoCRegister", iocRegister);
		
		return result;
	}
	
	
}
