export function ngDebounce(timeout: number) {
  // store timeout value for cancel the timeout
  let timeoutRef: any;

  return function(_target: any, _propertyKey: string, descriptor: PropertyDescriptor) {
    
    // store original function for future use
    const original = descriptor.value;

    // override original function
    descriptor.value = function(...args: any) {
      
      // clear previous timeout
      clearTimeout(timeoutRef);
      
      // sechudle timer
      setTimeout(() => {

        // call original function
        original.apply(this, args);

      }, timeout);
    }
    
    // return descriptor with new value
    return descriptor;
  }
}