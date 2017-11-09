node
{
    git credentialsId: '4b4913ce-511d-4fbf-ab51-48e5971ac3ee', url: 'git@bitbucket.org:ajo/labdsoft-2017-g1.git'
    
	stage ('Stage 1 - Checkout')
	echo '----------- Only checkout the code from the repository -----------' 
    checkout([$class: 'GitSCM', branches: [[name: '*/master']], doGenerateSubmoduleConfigurations: false, extensions: [], submoduleCfg: [], userRemoteConfigs: [[credentialsId: '4b4913ce-511d-4fbf-ab51-48e5971ac3ee', url: 'git@bitbucket.org:ajo/labdsoft-2017-g1.git']]])
 
    stage('Stage 2 - Build')
	echo '----------- Building solution -----------' 

    dir('GAM')
    {
        echo 'Building solution GAM.sln with MSBuild file'
        echo 'Using nuget to restore GAM.sln'
        bat 'nuget restore GAM.sln'
        echo  'Building'
        bat "\"msbuild\" GAM.sln /p:Configuration=Release /p:Platform=\"Any CPU\" /p:ProductVersion=1.0.0.${env.BUILD_NUMBER}"
	
	
	stage ('Stage 3 - Archive')
	echo '----------- Archiving files -----------'
	archive 'GAM/bin/Release/**'

    }   
        
}
    