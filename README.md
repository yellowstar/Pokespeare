# Global Expansion - Engineering Challenge

## Shakespearean Pokemon

The challenge has been written in C# and .Net Core Framework 3.1 using Microsoft Visual Studio 2019 Version 16.8.2

The solution is named Pokespeare and includes a representative set of unit tests.

## Running the Solution

A Dockerfile is included in the root directory of the solution.

To create the docker image open a PowerShell command window in the directory containing the Dockerfile and run the following command:

```console
docker build -t pokespeare .
```

Once the image has been successfully built user the following command to start it:

```console
docker run -d -p 8080:80 --name pokespeare pokespeare
```

Once the container is running simply point your favourite browser at:

http://localhost:8080/pokemon/{pokemon name}

replacing {pokemon name} with your favourite Pokemon

As an additional bonus a number of translators have also been included, these are:

- [Shakespeare]
- [Yoda]
- [Vulcan]

The shakespeare translator is the default if one is not selected, to try a different translation use the following:

http://localhost:8080/pokemon/{pokemon name}/{translation}

for example:

> http://localhost:8080/pokemon/pikachu/yoda

## Addendum

It was originally my intention to return a list of the flavour text entries for the requested Pokemon, but because of the restrictions on the publically available translation API the code takes just the first item. If more API calls were available the code could return multiple lines.
This configuration, together with the URLs of the API endpoints would be stored in a suitable configuration location if this were production code.
