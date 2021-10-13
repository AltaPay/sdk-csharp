# AltaPay - API C_Sharp

C_Sharp is a Client library that is used as a bridge between customer .Net solutions and Altapay gateway.

## Requirements

To build/package the C_Sharp you also need a number of build-tools.

Details about installing Mono can be found at http://www.mono-project.com/download/.

Below can be seen what had to be done in order to have a complete mono instalation on Ubuntu 16.04.

    $ sudo apt-key adv --keyserver hkp://keyserver.ubuntu.com:80 --recv-keys 3FA7E0328081BFF6A14DA29AA6A19B38D3D831EF
     echo "deb http://download.mono-project.com/repo/ubuntu xenial main" | sudo tee /etc/apt/sources.list.d/mono-official.list
    $ sudo apt-get update
  
  then (the complete installation of the library - for avoiding the "assembly not found" cases)
     
    $ sudo apt-get install mono-complete
    
 and finally:   
    
    $ sudo apt-get install ant

## How to Build

Start the build process by going to the repository directory from the terminal and run:

    $ ant

The package can also be built using bash script, which builds the Docker image and generates the zip package from source. Execute the script like

    $ ./docker/docker-build-script.sh

## Changelog

See [Changelog](CHANGELOG.md) for all the release notes.

## License

Distributed under the MIT License. See [LICENSE](LICENSE) for more information.

## Documentation

For more details please see [AltaPay docs](https://documentation.altapay.com/)

## Contact
Feel free to contact our support team (support@altapay.com) if you need any assistance.
