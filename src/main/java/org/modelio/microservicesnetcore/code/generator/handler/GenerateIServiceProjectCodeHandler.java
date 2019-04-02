package org.modelio.microservicesnetcore.code.generator.handler;

import java.io.File;
import java.io.FileWriter;
import java.util.List;

import org.modelio.metamodel.uml.statik.Classifier;
import org.modelio.metamodel.uml.statik.Operation;
import org.modelio.metamodel.uml.statik.Package;
import org.modelio.microservicesnetcore.code.generator.template.IServiceProjectTemplate;
import org.modelio.microservicesnetcore.helper.PimPsmMapper;
import org.modelio.modeliotools.treevisitor.HandlerAdapter;


public class GenerateIServiceProjectCodeHandler extends HandlerAdapter {
	private String _path;
	private IServiceProjectTemplate _template;
	List<String> _services;
	
	public GenerateIServiceProjectCodeHandler(String applicationName,Package domain,String path,List<String> services)
	{
		_services=services;
		_path=path+"\\IServices";
		_template=new IServiceProjectTemplate(applicationName, domain);
		// créer le répertoire project si il n'existe pas
		File fileDir = new File(_path);
		fileDir.mkdirs();
		
		// créer le csproj
		String name=applicationName+"."+domain.getName()+"Domain.IServices.csproj";
		
		StringBuffer content = new StringBuffer("");
		content.append(_template.getCsProj());
		if(content.length()>0)
		{
			try {
				File csprojFile =new File(_path+"\\"+name);
				csprojFile.createNewFile();
				FileWriter writer = new FileWriter(csprojFile);
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
		_services.add(visited.getName());
		String name="I"+visited.getName()+".cs";
		Classifier pimEnt = (Classifier)PimPsmMapper.GetPimFromPsmService(visited);
		if(pimEnt!=null)
		{
			Classifier entity = (Classifier)PimPsmMapper.GetPsmModelFromPim(pimEnt);
			if(entity!=null)
			{
				StringBuffer content = new StringBuffer("");
				content.append(_template.getHeader(visited,entity));
				
				for(Operation ope : visited.getOwnedOperation())
				{
					content.append(_template.getOperation(ope));
				}
				content.append(_template.getEnd());
				
				if(content.length()>0)
				{
					try {
						File csFile =new File(_path+"\\"+name);
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
