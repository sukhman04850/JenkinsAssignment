pipeline {

agent any

environment {

    IMAGE_NAME='ssk1811/jenkinstrial'

    IMAGE_TAG='latest'

}
stages {

stage('Checkout') {

steps {


checkout scm
//git 'https://github.com/sukhman04850/JenkinsTrial.git'

}

}



stage('Build') {

steps {



sh 'dotnet build'

}

}

stage('Docker Build'){
steps{
script{
docker.build("${IMAGE_NAME}:${IMAGE_TAG}")
}
}
}

stage('Docker Push'){

    steps{

        script{

            docker.withRegistry('https://index.docker.io/v1/','docker_login'){


                docker.image("${IMAGE_NAME}:${IMAGE_TAG}").push()

            }
        }

    }

}
stage('Run container') {

steps {

script{
sh 'docker rm -f ${IMAGE_NAME} || true '
    sh "docker run -d --name devops -p 8000:80 ${IMAGE_NAME}:${IMAGE_TAG}"
}

}

}
}

}
