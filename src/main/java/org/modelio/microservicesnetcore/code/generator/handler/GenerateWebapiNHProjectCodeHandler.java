package org.modelio.microservicesnetcore.code.generator.handler;

import java.io.File;
import java.io.FileWriter;
import java.io.IOException;
import java.io.InputStream;
import java.nio.file.Files;
import java.nio.file.Paths;
import java.nio.file.StandardCopyOption;
import java.util.List;

import org.modelio.metamodel.uml.statik.Classifier;
import org.modelio.metamodel.uml.statik.Package;
import org.modelio.microservicesnetcore.code.generator.template.WebapiNHProjectTemplate;
import org.modelio.microservicesnetcore.helper.PimPsmMapper;
import org.modelio.modeliotools.treevisitor.HandlerAdapter;


public class GenerateWebapiNHProjectCodeHandler extends HandlerAdapter {
	private String _path;
	private String _pathFromProperties;
	private String _pathToProperties;
	private String _pathFromappsettings;
	private String _pathToInfrastructure;
	private WebapiNHProjectTemplate _template;
	
	public GenerateWebapiNHProjectCodeHandler(String applicationName,Package domain,String path,List<String> services,List<String> mappingfiles)
	{
		_path=path+"\\WebapiNH";
		_pathFromProperties="org/modelio/microservicesnetcore/template/05 - webapi/Properties";
		_pathToProperties=_path+"\\Properties";
		_pathFromappsettings="org/modelio/microservicesnetcore/template/05 - webapi/config";
		_pathToInfrastructure=_path+"\\Infrastructure";
		_template=new WebapiNHProjectTemplate(applicationName, domain,mappingfiles);
		
		// créer le répertoire project si il n'existe pas
		File fileDir = new File(_path);
		fileDir.mkdirs();
		
		fileDir = new File(_path+"\\Controllers");
		fileDir.mkdirs();
		
		fileDir = new File(_pathToProperties);
		fileDir.mkdirs();
		
		// créer le csproj
		CreateProjectFile(applicationName, domain);
		
		// create Startup.cs
		CreateStartupFile(applicationName, domain,services);
		
		// create Program.cs
		CreateProgramFile(applicationName, domain);
		
		// create appsettings.json
		CreateAppSettingsFile();
		
		// create appsettings.json
		CreateInfrastructureFile();
				
		// create appsettings.json
		CreateNHibernateCfg();
		
		// create Properties\\launchSettings.json
		CreatePropertiesFolder();
	}

	private void CreateInfrastructureFile() {
		// TODO Auto-generated method stub
		String name="NhProxyJsonConverter.cs";
		StringBuffer content = new StringBuffer("");
		content.append(_template.getNhProxyJsonConverter());
		if(content.length()>0)
		{
			try {
				File file =new File(_pathToInfrastructure+"\\"+name);
				file.createNewFile();
				FileWriter writer = new FileWriter(file);
				try {
	                writer.write(content.toString());
	            } finally {
	                // quoiqu'il arrive, on ferme le fichier
	                writer.close();
	            }
	        } catch (Exception e) {
	            System.out.println("Impossible de creer le fichier : "+_path+"/"+name);
	        }
		}
	}

	private void CreateNHibernateCfg() {
		// TODO Auto-generated method stub
		String name="hibernate.cfg.xml";
		StringBuffer content = new StringBuffer("");
		content.append(_template.getHibernateCfg());
		if(content.length()>0)
		{
			try {
				File file =new File(_path+"\\"+name);
				file.createNewFile();
				FileWriter writer = new FileWriter(file);
				try {
	                writer.write(content.toString());
	            } finally {
	                // quoiqu'il arrive, on ferme le fichier
	                writer.close();
	            }
	        } catch (Exception e) {
	            System.out.println("Impossible de creer le fichier : "+_path+"/"+name);
	        }
		}		
	}

	private void CreateStartupFile(String applicationName, Package domain,List<String> services) {
		// TODO Auto-generated method stub
		String name="Startup.cs";
		StringBuffer content = new StringBuffer("");
		content.append(_template.getStartupCs(services));
		if(content.length()>0)
		{
			try {
				File file =new File(_path+"\\"+name);
				file.createNewFile();
				FileWriter writer = new FileWriter(file);
				try {
	                writer.write(content.toString());
	            } finally {
	                // quoiqu'il arrive, on ferme le fichier
	                writer.close();
	            }
	        } catch (Exception e) {
	            System.out.println("Impossible de creer le fichier : "+_path+"/"+name);
	        }
		}
	}

	private void CreateProgramFile(String applicationName, Package domain) {
		// TODO Auto-generated method stub
		String name="Program.cs";
		StringBuffer content = new StringBuffer("");
		content.append(_template.getProgramCs());
		if(content.length()>0)
		{
			try {
				File file =new File(_path+"\\"+name);
				file.createNewFile();
				FileWriter writer = new FileWriter(file);
				try {
	                writer.write(content.toString());
	            } finally {
	                // quoiqu'il arrive, on ferme le fichier
	                writer.close();
	            }
	        } catch (Exception e) {
	            System.out.println("Impossible de creer le fichier : "+_path+"/"+name);
	        }
		}
	}

	private void CreateAppSettingsFile() {
		// TODO Auto-generated method stub
		try{
			InputStream is = getClass().getClassLoader().getResourceAsStream(_pathFromappsettings+"/appsettings.json");
		    Files.copy(is, Paths.get(_path+"\\appsettings.json"),StandardCopyOption.REPLACE_EXISTING);
		    is = getClass().getClassLoader().getResourceAsStream(_pathFromappsettings+"/appsettings.Development.json");
		    Files.copy(is, Paths.get(_path+"\\appsettings.Development.json"),StandardCopyOption.REPLACE_EXISTING);
		}
		catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}

	private void CreatePropertiesFolder() {
		try{
			InputStream is = getClass().getClassLoader().getResourceAsStream(_pathFromProperties+"/launchSettings.json");
		    Files.copy(is, Paths.get(_pathToProperties+"\\launchSettings.json"),StandardCopyOption.REPLACE_EXISTING);
		}
		catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}

	private void CreateProjectFile(String applicationName, Package domain) {
		String name=applicationName+"."+domain.getName()+"Domain.Webapi.csproj";
		
		StringBuffer content = new StringBuffer("");
		content.append(_template.getCsProj());
		if(content.length()>0)
		{
			try {
				File file =new File(_path+"\\"+name);
				file.createNewFile();
				FileWriter writer = new FileWriter(file);
				try {
	                writer.write(content.toString());
	            } finally {
	                // quoiqu'il arrive, on ferme le fichier
	                writer.close();
	            }
	        } catch (Exception e) {
	            System.out.println("Impossible de creer le fichier : "+_path+"/"+name);
	        }
		}
	}
	
	@Override
	protected void beginVisitingClassifier(Classifier visited) 
	{
		String name=visited.getName()+".cs";
		Classifier pimEnt = (Classifier)PimPsmMapper.GetPimFromPsmControllerApi(visited);
		if(pimEnt!=null)
		{
			Classifier entity = (Classifier)PimPsmMapper.GetPsmModelFromPim(pimEnt);
			if(entity!=null)
			{
				StringBuffer content = new StringBuffer("");
				content.append(_template.getHeader(visited,entity));

// 		Non géré
//				for(Operation ope : visited.getOwnedOperation())
//				{
//					content.append(_template.getOperation(ope));
//				}
				
				content.append(_template.getEnd());
				
				if(content.length()>0)
				{
					try {
						File csFile =new File(_path+"\\Controllers\\"+name);
						csFile.createNewFile();
						FileWriter writer = new FileWriter(csFile);
						try 
						{
			                writer.write(content.toString());
			            } 
						finally 
						{
			                // quoiqu'il arrive, on ferme le fichier
			                writer.close();
			            }
			        }
					catch (Exception e) {
			            System.out.println("Impossible de creer le fichier : "+_path+"/"+name);
			        }
				}
			}
		}
	}
}
