package main

import (
	"context"
	"log"
)

type name string

var ContextName name

func main() {
	// data := "hello"
	data := data{
		name: "Bolo",
	}
	ctx := context.Background()

	ctx = authorize(data, ctx)

	name := ctx.Value(ContextName)

	log.Printf("Name is %s", name)
}

func authorize(i interface{}, ctx context.Context) context.Context {

	val, ok := i.(nameGetter)
	if ok {
		newCtx := context.WithValue(ctx, ContextName, val)
		return newCtx
	} else {
		newCtx := context.WithValue(ctx, ContextName, "Undefined")
		return newCtx
	}

}

type data struct {
	name string
}

func (d data) GetName() string {
	return d.name
}

type nameGetter interface {
	GetName() string
}
