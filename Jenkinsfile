pipeline 
{
    agent any
	stages
	{	
		stage('Stage 1 - Checkout')
		{	steps
			{
				git credentialsId: '4b4913ce-511d-4fbf-ab51-48e5971ac3ee', url: 'git@bitbucket.org:ajo/labdsoft-2017-g1.git'
				echo '----------- Only checkout the code from the repository -----------' 
				checkout([$class: 'GitSCM', branches: [[name: '*/master']], doGenerateSubmoduleConfigurations: false, extensions: [], submoduleCfg: [], userRemoteConfigs: [[credentialsId: '4b4913ce-511d-4fbf-ab51-48e5971ac3ee', url: 'git@bitbucket.org:ajo/labdsoft-2017-g1.git']]])
			}
		}
		
		stage('Stage 2 - Build')
		{
			steps
			{	
				echo '----------- Building solution ------------------------------------' 
				dir('GAM')
				{
					echo 'Building solution GAM.sln'
					bat 'dotnet build'
				}
			}
		}
	
		
		stage('Stage 3 - Unit Tests')
		{
			steps
			{
				echo '----------- Unit Tests -------------------------------------------'
				dir('GAM')
				{		
					script
					{
						echo '------- Build Test Project -------'
						bat 'dotnet test GamTests --no-build --logger=trx'									
					}
				}				
			}
		}		

		stage('Stage 4 - Publish Unit Tests Results')
		{
			steps
			{
				echo '----------- Publish Unit Tests Results -------------------------------------------'
				dir('GAM')
				{		
					script
					{	
						echo '------- Generate file using MSTestPublisher -------'
						step([$class: 'MSTestPublisher', UnitTestFile:"**/*.trx", failOnError: true, keepLongStdio: true])							
					}
				}	
			}
		}
		
		stage('Stage 5 - Archive')
		{
			steps
			{	
				echo '----------- Archiving files --------------------------------------'
				dir('GAM')
				{
					bat 'dotnet publish'
					archive '*/bin/Debug/netcoreapp2.0/**'
					
				}
			}
		}
		
		
		//stage('Stage 6 - Send Email Notification'){
		//	steps
		//	{	
		//		echo '----------- Sending a email -------------------------------------------'
		//		mail bcc: '', body: '''Pipeline without errors. Build successful.
		//		''', cc: '', from: '', replyTo: '', subject: 'Jenkins Pipeline GAM', to: 'mcorreialabdsoft@gmail.com, danielbento92@gmail.com, anacarolinafbarros@gmail.com, maria.marq.almeida@gmail.com, tiagoncalvesjenkins@gmail.com'
		//	}
		//}			
	} 
}