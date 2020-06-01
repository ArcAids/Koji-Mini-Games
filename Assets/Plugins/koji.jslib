mergeInto(LibraryManager.library, {

  Subscribe: function (type, fullKey) {
    if (typeof window !== undefined && typeof window.kojiBridge !== undefined && typeof window.kojiBridge.subscribe !== undefined) {
      window.kojiBridge.subscribe(Pointer_stringify(type), Pointer_stringify(fullKey));
    }
  }

});
