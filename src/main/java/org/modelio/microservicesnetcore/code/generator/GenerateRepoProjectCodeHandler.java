package org.modelio.microservicesnetcore.code.generator;

import java.io.File;
import java.io.FileWriter;
import java.util.List;

import org.modelio.metamodel.uml.statik.Attribute;
import org.modelio.metamodel.uml.statik.Classifier;
import org.modelio.metamodel.uml.statik.Operation;
import org.modelio.metamodel.uml.statik.Package;
import org.modelio.microservicesnetcore.helper.PimPsmMapper;
import org.modelio.modeliotools.treevisitor.HandlerAdapter;


public class GenerateRepoProjectCodeHandler extends HandlerAdapter {
	private String _path;
	private RepositoryProjectTemplate _template;
	
	public GenerateRepoProjectCodeHandler(String applicationName,Package domain,String path)
	{
		_path=path+"\\Repository";
		_template=new RepositoryProjectTemplate(applicationName, domain);
		// créer le répertoire project si il n'existe pas
		File fileDir = new File(_path);
		fileDir.mkdirs();
		
		// créer le csproj
		String name=applicationName+"."+domain.getName()+".Repository.csproj";
		
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
		String name=visited.getName()+".cs";
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
