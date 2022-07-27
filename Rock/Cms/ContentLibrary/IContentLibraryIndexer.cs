﻿// <copyright>
// Copyright by the Spark Development Network
//
// Licensed under the Rock Community License (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.rockrms.com/license
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
//

using System.Threading.Tasks;

using Rock.Attribute;

namespace Rock.Cms.ContentLibrary
{
    /// <summary>
    /// The methods that must be implemented for an entity to be able to
    /// participate in the content library system.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         <strong>This is an internal API</strong> that supports the Rock
    ///         infrastructure and not subject to the same compatibility standards
    ///         as public APIs. It may be changed or removed without notice in any
    ///         release and should therefore not be directly used in any plug-ins.
    ///     </para>
    /// </remarks>
    [RockInternal]
    internal interface IContentLibraryIndexer
    {
        /// <summary>
        /// Creates or updates an indexed document for the specified entity.
        /// </summary>
        /// <param name="id">The identifier of the entity to be indexed.</param>
        /// <param name="options">The options that describe this index operation request.</param>
        /// <returns>The number of documents that were indexed.</returns>
        Task<int> IndexContentLibraryDocumentAsync( int id, IndexDocumentOptions options );

        /// <summary>
        /// Deletes the specified entity from the index.
        /// </summary>
        /// <param name="id">The identifier of the entity to be deleted from the index.</param>
        Task DeleteContentLibraryDocumentAsync( int id );

        /// <summary>
        /// Creates or updates all documents that belong to the specified source.
        /// </summary>
        /// <param name="sourceId">The identifier of the source that should be indexed.</param>
        /// <param name="options">The options that describe this index operation request.</param>
        /// <returns>The number of documents that were indexed.</returns>
        Task<int> IndexAllContentLibrarySourceDocumentsAsync( int sourceId, IndexDocumentOptions options );

        /// <summary>
        /// Deletes all documents that belong to the specified source.
        /// </summary>
        /// <param name="sourceId">The identifier of the source whose documents should be deleted.</param>
        Task DeleteAllContentLibrarySourceDocumentsAsync( int sourceId );
    }
}