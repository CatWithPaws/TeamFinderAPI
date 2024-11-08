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
                nunit testResultsPattern: 'test/*.xml',failIfNoResults:'true',failedTestsFailBuild: 'true'
            }
        }
        stage('Deploy') {
            steps {
                echo 'Sending deploy trigger to remote server'
                script{
                    withCredentials([
                        secretText(
                        credentialsId: 'JenkinsTriggerURL',
                        variable: 'url')
                    ]) {
                        sh 'curl -I ' + url  
                    }
                             
            }
        }
    }
}
