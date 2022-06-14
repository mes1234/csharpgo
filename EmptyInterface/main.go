package main

import (
	"context"
	"log"
)

func main() {

	arr := make([]interface{}, 2)

	arr[0] = "hello"

	arr[1] = data{
		name: "Bolo",
	}

	for _, item := range arr {
		ctx := context.Background()

		ctx = authorize(item, ctx)

		name := ctx.Value("Name")

		log.Printf("Name is %s", name)
	}
}

func authorize(i interface{}, ctx context.Context) context.Context {

	val, ok := i.(nameGetter)
	if ok {
		newCtx := context.WithValue(ctx, "Name", val)
		return newCtx
	}

	newCtx := context.WithValue(ctx, "Name", "Undefined")
	return newCtx

}

type nameGetter interface {
	GetName() string
}

type data struct {
	name string
}

func (d data) GetName() string {
	return d.name
}
