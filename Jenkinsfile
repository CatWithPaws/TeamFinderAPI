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
                sh 'curl -I http://admin:admin@192.168.0.200:8080/job/Production%20Server/build?token=sasdagagasfasfasfasfagasdasd'                
                }
            }
    }
}
