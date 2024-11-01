pipeline {
    agent any

    stages {
        stage('Build') {
            steps {
                sh 'dotnet build'
                nunit testResultsPattern: 'tests/*.xml'
            }
        }
        stage('Test') {
            steps {
                echo 'Testing..'
                echo 'testing 123123123123123'
            }
        }
        stage('Deploy') {
            steps {
                echo 'Deploying....'
                echo 'success'
            }
        }
    }
}