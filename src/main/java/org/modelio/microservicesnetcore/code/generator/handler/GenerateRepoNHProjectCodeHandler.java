package org.modelio.microservicesnetcore.code.generator.handler;

import java.io.File;
import java.io.FileWriter;
import java.util.List;

import org.modelio.metamodel.uml.statik.Classifier;
import org.modelio.metamodel.uml.statik.Operation;
import org.modelio.metamodel.uml.statik.Package;
import org.modelio.microservicesnetcore.code.generator.template.RepositoryNHProjectTemplate;
import org.modelio.microservicesnetcore.helper.PimPsmMapper;
import org.modelio.modeliotools.treevisitor.HandlerAdapter;


public class GenerateRepoNHProjectCodeHandler extends HandlerAdapter {
	private String _path;
	private RepositoryNHProjectTemplate _template;
	private List<String> _mappingFiles;
	
	public GenerateRepoNHProjectCodeHandler(String applicationName,Package domain,String path,List<String> mappingFiles)
	{
		_path=path+"\\RepositoriesNH";
		_template=new RepositoryNHProjectTemplate(applicationName, domain);
		 _mappingFiles=mappingFiles;
		
		// créer le répertoire project si il n'existe pas
		File fileDir = new File(_path);
		fileDir.mkdirs();
		
		// créer le csproj
		String name=applicationName+"."+domain.getName()+"Domain.RepositoriesNH.csproj";
		
		StringBuffer content = new StringBuffer("");
		content.append(_template.getCsProj());
		String fileContent = content.toString().replace("@@embeddedresource",_template.getEmbedded(_mappingFiles));
		
		if(content.length()>0)
		{
			try {
				File csprojFile =new File(_path+"\\"+name);
				csprojFile.createNewFile();
				FileWriter writer = new FileWriter(csprojFile);
				try {
	                writer.write(fileContent);
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
		String name=visited.getName()+"NH.cs";
		Classifier pimEnt = (Classifier)PimPsmMapper.GetPimFromPsmRepository(visited);
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
