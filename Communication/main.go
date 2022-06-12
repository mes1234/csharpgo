package main

import (
	"context"
	"log"
	"time"
)

func main() {

	channel := make(chan string)

	ctx, cancel := context.WithCancel(context.Background())

	go recieve(channel, ctx)

	go produce(channel, ctx)

	time.Sleep(5 * time.Second)

	cancel()

	time.Sleep(1 * time.Second)

	log.Printf("Exiting...")

}

func produce(output chan<- string, ctx context.Context) {
	for {
		select {
		case <-ctx.Done():
			log.Printf("Time to stop producing...")
			return
		case <-time.After(1 * time.Second):
			log.Printf("Sending data data ...")
			output <- "new very important data sent..."
		}
	}
}

func recieve(input <-chan string, ctx context.Context) {
	for {
		select {
		case <-ctx.Done():
			log.Printf("Time to stop recieving ...")
			return
		case data := <-input:
			log.Printf("recieved: \"%s\"", data)
		case <-time.After(1 * time.Second):
			log.Printf("No data in 1sec...")
		}
	}
}
