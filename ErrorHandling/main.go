package main

import (
	"io/ioutil"
	"log"
	"net/http"
	"strconv"
)

func main() {

	variable := "1"

	converted, errParse := strconv.Atoi(variable)

	if errParse != nil {
		log.Fatalf("cannot conver error: %s\n", errParse.Error())
	}

	log.Printf("Hurray I can add 1 to %d and get %d\n", converted, converted+1)

	response, errHttp := http.Get("http://worldtimeapi.org/api/ip")

	if errHttp != nil {
		log.Fatalf("cannot get data error: %s\n", errHttp.Error())
	}

	body, errDecoding := ioutil.ReadAll(response.Body)

	if errDecoding != nil {
		log.Fatalf("cannot decode error %s\n", errDecoding.Error())
	}

	log.Printf("Current time here and now is %s\n", body)
}
