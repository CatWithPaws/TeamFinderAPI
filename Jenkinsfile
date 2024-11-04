pipeline {
    agent any

    stages {
        stage('Build') {
            steps {
                sh 'dotnet build'
                
            }
        }
        stage('Test') {
            steps {
                echo 'Testing..'
                sh 'dotnet test --results-directory ./test --logger:"nunit;LogFileName=testResults.xml"'
                nunit {testResultsPattern: 'test/*.xml' ,failIfNoResults: true, failedTestsFailBuild: true}
            }
        }
        stage('Deploy') {
            steps {
                echo 'Deploying....'
                script{
                    try{
                        sh "./proddeploy.sh"
                        echo 'Deployed successfully'
                    }
                    catch{
                        currentBuild.result = 'FAILURE'
                    }
                }
            }
        }
    }
}